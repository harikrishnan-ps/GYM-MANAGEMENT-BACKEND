using GymManagement.Api.Data;
using GymManagement.Api.Middleware;
using GymManagement.Api.Repositories.Implementations;
using GymManagement.Api.Repositories.Interfaces;
using GymManagement.Api.Services.Implementations;
using GymManagement.Api.Services.Interfaces;
using GymManagement.Api.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. Database Connection (MySQL)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<GymDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// 2. JWT Authentication configuration
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"] ?? "SuperSecretGymManagementAPIKey12345!@#$");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Set to true in production
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"] ?? "GymManagementApi",
        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"] ?? "GymManagementClient",
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// 3. Dependency Injection Setup
// Repositories
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IGymClassRepository, GymClassRepository>();
builder.Services.AddScoped<IRevenueRepository, RevenueRepository>();

// Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IGymClassService, GymClassService>();
builder.Services.AddScoped<IRevenueService, RevenueService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

// AutoMapper
var mapperConfig = new AutoMapper.MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfiles());
});
AutoMapper.IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// 4. CORS Policy Setup
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularClient", policy =>
    {
        policy.WithOrigins("http://localhost:8080") // Angular default port
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// 5. Controllers & Endpoints
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// 6. Swagger Setup with JWT Support
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gym Management API", Version = "v1" });

    // Define security scheme
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer token **only** (do not prepend 'Bearer ')",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, Array.Empty<string>() }
    });
});

var app = builder.Build();

// 7. Middlewares Lifecycle
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gym Management API v1");
    });
}

// app.UseHttpsRedirection(); // Uncomment if HTTPS is configured locally

app.UseCors("AllowAngularClient");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

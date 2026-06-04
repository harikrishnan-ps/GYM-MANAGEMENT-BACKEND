using GymManagement.Api.DTOs.GymClass;
using GymManagement.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GymManagement.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/classes")]
    public class GymClassesController : ControllerBase
    {
        private readonly IGymClassService _classService;

        public GymClassesController(IGymClassService classService)
        {
            _classService = classService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var classes = await _classService.GetAllClassesAsync();
            return Ok(classes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var gymClass = await _classService.GetClassByIdAsync(id);
            if (gymClass == null)
            {
                return NotFound(new { message = $"Class with ID {id} not found." });
            }
            return Ok(gymClass);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClassDto createClassDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newClass = await _classService.CreateClassAsync(createClassDto);
            return CreatedAtAction(nameof(GetById), new { id = newClass.ClassId }, newClass);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateClassDto updateClassDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await _classService.UpdateClassAsync(id, updateClassDto);
            if (!updated)
            {
                return NotFound(new { message = $"Class with ID {id} not found." });
            }

            return Ok(new { message = "Class updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _classService.DeleteClassAsync(id);
            if (!deleted)
            {
                return NotFound(new { message = $"Class with ID {id} not found." });
            }

            return Ok(new { message = "Class deleted successfully." });
        }
    }
}

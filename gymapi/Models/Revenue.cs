using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymManagement.Api.Models
{
    [Table("Revenue")]
    public class Revenue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RevenueId { get; set; }

        [Required]
        [StringLength(100)]
        public string MemberName { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; } = string.Empty;

        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    }
}

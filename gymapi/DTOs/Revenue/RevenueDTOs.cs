using System;
using System.ComponentModel.DataAnnotations;

namespace GymManagement.Api.DTOs.Revenue
{
    public class RevenueDto
    {
        public int RevenueId { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
    }

    public class CreateRevenueDto
    {
        [Required]
        [StringLength(100)]
        public string MemberName { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 1000000.00, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; } = string.Empty;
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace GymManagement.Api.DTOs.Member
{
    public class MemberDto
    {
        public int MemberId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MembershipType { get; set; } = string.Empty;
        public DateTime JoinDate { get; set; }
    }

    public class CreateMemberDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string MembershipType { get; set; } = string.Empty;

        [Required]
        public DateTime JoinDate { get; set; }
    }
}

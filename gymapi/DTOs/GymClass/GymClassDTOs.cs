using System.ComponentModel.DataAnnotations;

namespace GymManagement.Api.DTOs.GymClass
{
    public class ClassDto
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public string TrainerName { get; set; } = string.Empty;
        public string ScheduleTime { get; set; } = string.Empty;
        public int Capacity { get; set; }
    }

    public class CreateClassDto
    {
        [Required]
        [StringLength(100)]
        public string ClassName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string TrainerName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string ScheduleTime { get; set; } = string.Empty;

        [Required]
        [Range(1, 1000, ErrorMessage = "Capacity must be at least 1.")]
        public int Capacity { get; set; }
    }
}

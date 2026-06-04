using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymManagement.Api.Models
{
    [Table("GymClasses")]
    public class GymClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClassId { get; set; }

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
        public int Capacity { get; set; }
    }
}

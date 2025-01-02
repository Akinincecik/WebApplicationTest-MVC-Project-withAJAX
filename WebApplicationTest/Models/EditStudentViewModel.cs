using System.ComponentModel.DataAnnotations;

namespace WebApplicationTest.Models
{
    public class EditStudentViewModel()
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "FullName is reguired.")]
        [StringLength(50, ErrorMessage = "FullName is too long")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Surname is reguired.")]
        [StringLength(50, ErrorMessage = "Surname is too long")]
        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string? MainLanguage { get; set; }
        public string? ForeignLanguage { get; set; }
        public bool Locked { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        //public string? ProfileChangeFileName { get; set; } = "no-image-icon-512x512-lfoanl0w.png";

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "student";
        public string Class { get; set; }
        public string? Done { get; set; }
    }
}

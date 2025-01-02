using System.ComponentModel.DataAnnotations;

namespace WebApplicationTest.Models
{
    public class StudentViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public string? FullName { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string? MainLanguage { get; set; }
        public string? ForeignLanguage { get; set; }
        public bool Locked { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? ProfileChangeFileName { get; set; } = "no-image-icon-512x512-lfoanl0w.png";
        public string Role { get; set; } = "user";
        public string Class { get; set; }

        //[Key]
        //public Guid Id { get; set; }

        //[StringLength(50)]
        //public string FullName { get; set; }

        //[Required]
        //[StringLength(30)]
        //public string Surname { get; set; }

        //[Required]
        //public DateTime DateOfBirth { get; set; }

        //[Required]
        //public string Gender { get; set; }

        //public string? MainLanguage { get; set; }
        //public string? ForeignLanguage { get; set; }

        //[Required]
        //[StringLength(100)]
        //public string? Password { get; set; }

        //public bool Locked { get; set; } = false;
        //public DateTime CreatedAt { get; set; } = DateTime.Now;

        //[StringLength(255)]
        //public string? ProfileChangeFileName { get; set; } = "no-image-icon-512x512-lfoanl0w.png";

        //[Required]
        //[StringLength(50)]
        //public string Role { get; set; } = "Student";
    }
}

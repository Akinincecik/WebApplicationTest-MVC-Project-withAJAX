using System.ComponentModel.DataAnnotations;

namespace WebApplicationTest.Models
{
    public class CreateStudentViewModel
    {
        //[Display(Name ="Kullanıcı Adı", Prompt ="johndoe")]
        //[Required(ErrorMessage = "Username is reguired.")]
        //[StringLength(50, ErrorMessage = "Username is too long")]
        //public string Username { get; set; }

        //[Display(Name ="Kullanıcı Adı", Prompt ="johndoe")]
        [Required(ErrorMessage = "FullName is reguired.")]
        [StringLength(50, ErrorMessage = "FullName is too long")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "FullName is reguired.")]
        [StringLength(50, ErrorMessage = "FullName is too long")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Date of birth is reguired.")]
        public DateTime DateOfBirth { get; set; }
        public bool Locked { get; set; }

        [Required(ErrorMessage = "Gender is reguired.")]
        public string Gender { get; set; }

        public string MainLanguage { get; set; }
        public string ForeignLanguage { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is reguired.")]
        [MaxLength(16, ErrorMessage = "Password needs to be less than 16 character.")]
        [MinLength(6, ErrorMessage = "Password needs to be at least 6 character.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "RePassword is reguired.")]
        [MaxLength(16, ErrorMessage = "RePassword needs to be less than 16 character.")]
        [MinLength(6, ErrorMessage = "RePassword needs to be at least 6 character.")]
        [Compare(nameof(Password))]
        public string RePassword { get; set; }

        [Required(ErrorMessage = "Class is reguired.")]
        public string Class { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "user";

        public string? Done { get; set; }
    }
}

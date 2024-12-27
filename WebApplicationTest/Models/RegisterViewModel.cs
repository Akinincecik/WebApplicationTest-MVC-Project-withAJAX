using System.ComponentModel.DataAnnotations;

namespace WebApplicationTest.Models
{
    public class RegisterViewModel
    {
        //[Display(Name ="Kullanıcı Adı", Prompt ="johndoe")]
        [Required(ErrorMessage = "Username is reguired.")]
        [StringLength(50, ErrorMessage = "Username is too long")]
        public string Username { get; set; }

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
    }
}

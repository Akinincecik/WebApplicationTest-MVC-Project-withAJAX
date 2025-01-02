using System.ComponentModel.DataAnnotations;

namespace WebApplicationTest.Models
{
    public class EditUserViewModel
    {
        public Guid Id { get; set; }

        //[Display(Name ="Kullanıcı Adı", Prompt ="johndoe")]
        [Required(ErrorMessage = "Username is reguired.")]
        [StringLength(50, ErrorMessage = "Username is too long")]
        public string Username { get; set; }

        //[Display(Name ="Kullanıcı Adı", Prompt ="johndoe")]
        [Required(ErrorMessage = "FullName is reguired.")]
        [StringLength(50, ErrorMessage = "FullName is too long")]
        public string FullName { get; set; }

        public bool Locked { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "user";

        public string? Done { get; set; }
    }
}

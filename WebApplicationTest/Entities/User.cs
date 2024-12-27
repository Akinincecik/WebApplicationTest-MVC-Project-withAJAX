using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace WebApplicationTest.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(50)]
        public string? FullName { get; set; }

        [Required]
        [StringLength(30)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        public bool Locked { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [StringLength(255)]
        public string? ProfileChangeFileName { get; set; } = "no-image-icon-512x512-lfoanl0w.png";

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "user";
    }
}

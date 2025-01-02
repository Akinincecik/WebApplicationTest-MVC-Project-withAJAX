using System.ComponentModel.DataAnnotations;

namespace WebApplicationTest.Models
{
    public class UserViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public string? FullName { get; set; }

        public string Username { get; set; }

        public bool Locked { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? ProfileChangeFileName { get; set; } = "no-image-icon-512x512-lfoanl0w.png";
        public string Role { get; set; } = "user";
    }
}

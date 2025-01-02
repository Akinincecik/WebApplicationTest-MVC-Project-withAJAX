using Microsoft.EntityFrameworkCore;

namespace WebApplicationTest.Entities
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<StudentInfo> StudentInformations { get; set; }
    }
}

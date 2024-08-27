using Microsoft.EntityFrameworkCore;

namespace DnD_Master.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Monster> Monsters { get; set; }
    }
}

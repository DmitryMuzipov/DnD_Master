using Microsoft.EntityFrameworkCore;
using DnD_Master.Models;

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
        public DbSet<DnD_Master.Models.Scene> Scene { get; set; } = default!;
    }
}

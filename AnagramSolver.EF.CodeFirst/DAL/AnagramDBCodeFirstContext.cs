using Microsoft.EntityFrameworkCore;
using AnagramSolver.EF.CodeFirst.Models;

namespace AnagramSolver.EF.CodeFirst.DAL
{
    public class AnagramDBCodeFirstContext : DbContext
    {
        public AnagramDBCodeFirstContext()
        {
        }

        public AnagramDBCodeFirstContext(DbContextOptions<AnagramDBCodeFirstContext> options)
            : base(options)
        {
        }

        public DbSet<WordEnt> Word { get; set; }
        public DbSet<CachedWord> CachedWord { get; set; }
        public DbSet<UserLog> UserLog { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=LT-LIT-SC-0505;Database=AnagramDBCodeFirst;Integrated security=true");
            }
        }
    }
}

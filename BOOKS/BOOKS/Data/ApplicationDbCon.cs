using BOOKS.Models;
using Microsoft.EntityFrameworkCore;
namespace BOOKS.Data
{
    public class ApplicationDbCon : DbContext
    {
        public ApplicationDbCon(DbContextOptions<ApplicationDbCon> options) : base(options)
        {

        }
        public DbSet<ClassicBooks> ClassicBooks { get; set; }
        public DbSet<FantasticBooks> FantasticBooks { get; set; }
        public DbSet<DystopianBooks> DystopianBooks { get; set; }
        public DbSet<PoemBooks> PoemBooks { get; set; }
        public DbSet<PhilosophyBooks> PhilosophyBooks { get; set; }


    }
}

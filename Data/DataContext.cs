using Microsoft.EntityFrameworkCore;

namespace SuperHeroAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        // Use code first migration, to see representation in the db we have to set the Model
        public DbSet<SuperHero> SuperHeroes { get; set; }
    }


}

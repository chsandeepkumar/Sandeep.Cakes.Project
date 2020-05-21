using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Sandeep.CakeDesign.WebApp.DataAccessLayer;

namespace Sandeep.CakeDesign.Repository.DataAccessLayer
{
    public class RecipesDbContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }

        public RecipesDbContext(DbContextOptions<RecipesDbContext> options)
            : base(options)
        {
            this.EnsureSeedData();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>(etb =>
            {
                etb.Property(e => e.Id);
                etb.HasKey(e => e.Id);
            });
        }
       
    }
}

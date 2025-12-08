using Microsoft.EntityFrameworkCore;
using RecipeManager.Models;

namespace RecipeManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
                public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Recipe>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.OwnerUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

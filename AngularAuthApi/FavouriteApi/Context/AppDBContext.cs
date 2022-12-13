using FavouriteApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FavouriteApi.Context
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext>options):base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Favourite> Favorites { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("users").HasMany(f=>f.favourites).WithOne(u=>u.User);
            modelBuilder.Entity<Favourite>().ToTable("favourites");

        }
    }
}

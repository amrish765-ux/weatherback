using AngularAuthApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularAuthApi.Context
{
    public class AppDbcontext:DbContext
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options):base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Favourite> Favorites { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Favourite>().ToTable("favourites");

        }
    }
}

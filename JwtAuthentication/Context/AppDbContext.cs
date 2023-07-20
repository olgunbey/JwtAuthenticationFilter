using JwtAuthentication.Models;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthentication.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            
        }
        public DbSet<Users> Users{ get; set; }
        public DbSet<Claims> Claims { get; set; }
        public DbSet<LoginInUsers> LoginInUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasOne(x => x.Claims).WithMany(x => x.Users).HasForeignKey(x => x.ClaimsID);
            modelBuilder.Entity<LoginInUsers>().HasOne(x => x.Claims).WithMany(x => x.LoginInUsers).HasForeignKey(x => x.ClaimsID);
        }

    }
}

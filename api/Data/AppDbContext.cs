using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<AppUser> AppUser {get; set;}
    public DbSet<NewsSiteModel> NewsSite {get; set;}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        
        // SEED NEWS SITES DATA INFO HERE

        List<IdentityRole> RolesList = new List<IdentityRole>
        {
            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER"
            }
        };

        builder.Entity<IdentityRole>().HasData(RolesList);

        base.OnModelCreating(builder);
    }
}

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
        List<NewsSiteModel> ListToBeSeeded = new List<NewsSiteModel>
        {
            new NewsSiteModel 
            {
                Id = Guid.NewGuid(),
                NewSiteName = "LaNacion",
                Siteurl = "https://www.lanacion.com.ar",
                SiteUrlQueryString = "https://www.lanacion.com.ar/buscador/?query=",
                ContainerClassName = "queryly_item_row",
                TitleClassName = "queryly_item_title",
                IsSearchHttpGetBased = true,
                PoliticalLeaning = "Center-Right"
            },
            new NewsSiteModel
            {
                Id = Guid.NewGuid(),
                NewSiteName = "TodoNoticias",
                Siteurl = "https://tn.com.ar",
                SiteUrlQueryString = "https://tn.com.ar/buscar/",
                ContainerClassName = "card__container",
                TitleClassName = "card__headline",
                IsSearchHttpGetBased = true,
                PoliticalLeaning = "Center-Right"
            },
            new NewsSiteModel
            {
                Id = Guid.NewGuid(),
                NewSiteName = "A24",
                Siteurl = "https://www.a24.com",
                SiteUrlQueryString = "https://www.a24.com/contenidos/resultado.html?search=",
                ContainerClassName = "gs-webResult",
                TitleClassName = "gs-title",
                IsSearchHttpGetBased = true,
                PoliticalLeaning = "Center",
            },
            new NewsSiteModel
            {
                Id = Guid.NewGuid(),
                NewSiteName = "Pagina12",
                Siteurl = "https://www.pagina12.com.ar",
                SiteUrlQueryString = "https://www.pagina12.com.ar/buscar?q=",
                ContainerClassName = "article-item",
                TitleClassName = "title",
                IsSearchHttpGetBased = true,
                PoliticalLeaning = "Left"
            },
            new NewsSiteModel
            {
                Id = Guid.NewGuid(),
                NewSiteName = "ElDestape",
                Siteurl = "https://www.eldestapeweb.com",
                SiteUrlQueryString = "https://www.eldestapeweb.com/buscar/",
                ContainerClassName = "item-3",
                TitleClassName = "titulo",
                IsSearchHttpGetBased = true,
                PoliticalLeaning = "Left"
            }
        };
        
        builder.Entity<NewsSiteModel>().HasData(ListToBeSeeded); 

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

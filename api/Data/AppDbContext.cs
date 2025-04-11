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

    public DbSet<NewsSiteSearchInfoModel> NewsSiteSearchInfo {get; set;}

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
                IsSearchHttpGetBased = true,
                PoliticalLeaning = "Center-Right"
            },
            new NewsSiteModel
            {
                Id = Guid.NewGuid(),
                NewSiteName = "TodoNoticias",
                Siteurl = "https://tn.com.ar",
                SiteUrlQueryString = "https://tn.com.ar/buscar/",
                IsSearchHttpGetBased = true,
                PoliticalLeaning = "Center-Right"
            },
            new NewsSiteModel
            {
                Id = Guid.NewGuid(),
                NewSiteName = "A24",
                Siteurl = "https://www.a24.com",
                SiteUrlQueryString = "https://www.a24.com/contenidos/resultado.html?search=",
                IsSearchHttpGetBased = true,
                PoliticalLeaning = "Center",
            },
            new NewsSiteModel
            {
                Id = Guid.NewGuid(),
                NewSiteName = "Pagina12",
                Siteurl = "https://www.pagina12.com.ar",
                SiteUrlQueryString = "https://www.pagina12.com.ar/buscar?q=",
                IsSearchHttpGetBased = true,
                PoliticalLeaning = "Left"
            },
            new NewsSiteModel
            {
                Id = Guid.NewGuid(),
                NewSiteName = "ElDestape",
                Siteurl = "https://www.eldestapeweb.com",
                SiteUrlQueryString = "https://www.eldestapeweb.com/buscar/",
                IsSearchHttpGetBased = true,
                PoliticalLeaning = "Left"
            }
        };

        List<NewsSiteSearchInfoModel>  MainNewsInfoToSeed = new List<NewsSiteSearchInfoModel>
        {
            new NewsSiteSearchInfoModel
            {
                Id = Guid.NewGuid(),
                NewsSiteName = "LaNacion",
                isMainSiteNews = true,
                NewsContainerClassName = "grid-item",
                TitleClassName = "title",
                DescriptionClassName = "subhead",
                NewsImageClassName = "image"
            },
            new NewsSiteSearchInfoModel
            {
                Id = Guid.NewGuid(),
                NewsSiteName = "TodoNoticias",
                isMainSiteNews = true,
                NewsContainerClassName = "brick_one",
                TitleClassName = "card__headline",
                DescriptionClassName = "card__subheadline",
                NewsImageClassName = "image"
            },
            new NewsSiteSearchInfoModel
            {
                Id = Guid.NewGuid(),
                NewsSiteName = "A24",
                isMainSiteNews = true,
                NewsContainerClassName = "news-item",
                TitleClassName = "news-title",
                DescriptionClassName = "ignore-parser",
                NewsImageClassName = null
            },
            new NewsSiteSearchInfoModel
            {
                Id = Guid.NewGuid(),
                NewsSiteName = "Pagina12",
                isMainSiteNews = true,
                NewsContainerClassName = "headline-card",
                TitleClassName = "title",
                DescriptionClassName = "title-suffix",
                NewsImageClassName = "image"
            },
            new NewsSiteSearchInfoModel
            {
                Id = Guid.NewGuid(),
                NewsSiteName = "ElDestape",
                isMainSiteNews = true,
                NewsContainerClassName = "item-6",
                TitleClassName = "titulo",
                DescriptionClassName = null,
                NewsImageClassName = "lazyautosizes"
            },
        };


        List<NewsSiteSearchInfoModel>  NewsInfoToSeed = new List<NewsSiteSearchInfoModel>
        {
            new NewsSiteSearchInfoModel
            {
                Id = Guid.NewGuid(),
                NewsSiteName = "LaNacion",
                isMainSiteNews = false,
                NewsContainerClassName = "queryly_item_row",
                TitleClassName = "queryly_item_title",
                DescriptionClassName = "queryly_item_description",
                NewsImageClassName = null
            },
            new NewsSiteSearchInfoModel
            {
                Id = Guid.NewGuid(),
                NewsSiteName = "TodoNoticias",
                isMainSiteNews = false,
                NewsContainerClassName = "card__container",
                TitleClassName = "card__headline",
                DescriptionClassName = "card__subheadline",
                NewsImageClassName = "image"
            },
            new NewsSiteSearchInfoModel
            {
                Id = Guid.NewGuid(),
                NewsSiteName = "A24",
                isMainSiteNews = false,
                NewsContainerClassName = "gsc-webResult",
                TitleClassName = "gs-title",
                DescriptionClassName = "gs-bidi-start-align",
                NewsImageClassName = "gs-image"
            },
            new NewsSiteSearchInfoModel
            {
                Id = Guid.NewGuid(),
                NewsSiteName = "Pagina12",
                isMainSiteNews = false,
                NewsContainerClassName = "article-item",
                TitleClassName = "title",
                DescriptionClassName = "p12-separator--left--primary",
                NewsImageClassName = "image"
            },
            new NewsSiteSearchInfoModel
            {
                Id = Guid.NewGuid(),
                NewsSiteName = "ElDestape",
                isMainSiteNews = false,
                NewsContainerClassName = "item-3",
                TitleClassName = "titulo",
                DescriptionClassName = null,
                NewsImageClassName = "lazyautosizes"
            },
        };

        builder.Entity<NewsSiteModel>().HasData(ListToBeSeeded); 
        builder.Entity<NewsSiteSearchInfoModel>().HasData(NewsInfoToSeed);
        builder.Entity<NewsSiteSearchInfoModel>().HasData(MainNewsInfoToSeed);
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

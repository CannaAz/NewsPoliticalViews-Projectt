using api.Data;
using api.Dtos.NewsDtos;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace api.Controllers;

[Route("api/news")]
[ApiController]
public class NewsController : Controller
{
    private readonly IWebDriver _driver;
    private readonly AppDbContext _dbContext;

    public NewsController(IWebDriver driver, AppDbContext context)
    {
        _driver = driver;
        _dbContext = context;
    }

    // Gets the news from all sites that will appear if you searched the same query in them
    [HttpGet("GetNewsFromAllSites")]
    public async Task<IActionResult> GetFullInfo(string query)
    {
        try
        {
            if(!ModelState.IsValid) return BadRequest();
            
            List<NewsSiteModel> AllNewsSites = await _dbContext.NewsSite.Where(site => 
                site.IsSearchHttpGetBased == true &&
                site.SiteUrlQueryString != null
            ).ToListAsync();

            List<NewsInfoDto> AllInfo = new List<NewsInfoDto>();
            NewsSiteSearchInfoModel InfoModel = new NewsSiteSearchInfoModel();
            
            foreach(NewsSiteModel NewsSite in AllNewsSites)
            {
                await _driver.Navigate().GoToUrlAsync(NewsSite.SiteUrlQueryString + query);
                InfoModel = await _dbContext.NewsSiteSearchInfo.FirstOrDefaultAsync(info => info.NewsSiteName == NewsSite.NewSiteName && info.isMainSiteNews == false);

                IWebElement element = _driver.FindElement(By.ClassName(InfoModel.NewsContainerClassName));


                string? title = InfoModel.TitleClassName != null ? element.FindElement(By.ClassName(InfoModel.TitleClassName)).Text : "";
                string? descriptiom = InfoModel.DescriptionClassName != null ? element.FindElement(By.ClassName(InfoModel.DescriptionClassName)).Text : "";

                string? url = element.FindElement(By.TagName("a")).GetAttribute("href").ToString() ?? "Url Not Found";
                string? ImageUrl = InfoModel.NewsImageClassName != null ? element.FindElement(By.TagName("img")).GetAttribute("src").ToString() : "" ;

                

                NewsInfoDto NewsDto = new NewsInfoDto
                (
                    InfoModel.NewsSiteName,
                    title,
                    descriptiom,
                    url,
                    ImageUrl,
                    NewsSite.PoliticalLeaning
                );

                AllInfo.Add(NewsDto);
            }

            return Ok(AllInfo);
        }catch(Exception ex)
        {
            Console.WriteLine("An error has ocurred: \n");
            Console.WriteLine(ex.Message);

            return StatusCode(500);
        }
    }

    // basically get the first news that appear in the news sites and returns them to the user
    [HttpGet("CurrentlyImportant")]
    public async Task<IActionResult> CurrentlyImportant()
    {
        try
        {
            if(!ModelState.IsValid) return BadRequest();
            
            List<NewsSiteModel> AllNewsSites = await _dbContext.NewsSite.Where(site => 
                site.IsSearchHttpGetBased == true &&
                site.SiteUrlQueryString != null
            ).ToListAsync();

            List<NewsInfoDto> AllInfo = new List<NewsInfoDto>();
            NewsSiteSearchInfoModel InfoModel = new NewsSiteSearchInfoModel();
            
            foreach(NewsSiteModel NewsSite in AllNewsSites)
            {
                await _driver.Navigate().GoToUrlAsync(NewsSite.Siteurl);
                InfoModel = await _dbContext.NewsSiteSearchInfo.FirstOrDefaultAsync(info => info.NewsSiteName == NewsSite.NewSiteName && info.isMainSiteNews == true);


                IWebElement element = _driver.FindElement(By.ClassName(InfoModel.NewsContainerClassName));

    
                string? title = InfoModel.TitleClassName != null ? element.FindElement(By.ClassName(InfoModel.TitleClassName)).Text : "";
                string? descriptiom = InfoModel.DescriptionClassName != null ? element.FindElement(By.ClassName(InfoModel.DescriptionClassName)).Text : "";

                string? url = element.FindElement(By.TagName("a")).GetAttribute("href").ToString() ?? "Url Not Found";
                string? ImageUrl = InfoModel.NewsImageClassName != null ? element.FindElement(By.TagName("img")).GetAttribute("src").ToString() : "" ;

                NewsInfoDto NewsDto = new NewsInfoDto
                (
                    InfoModel.NewsSiteName,
                    title,
                    descriptiom,
                    url,
                    ImageUrl,
                    NewsSite.PoliticalLeaning
                );


                AllInfo.Add(NewsDto);
            }

            return Ok(AllInfo);
        }catch(Exception ex)
        {
            Console.WriteLine("An error has ocurred: \n");
            Console.WriteLine(ex.Message);

            return StatusCode(500);
        }
    }

    [HttpGet("GetAllNewsSites")]
    public async Task<IActionResult> GetAllNewsSites()
    {
        if(!ModelState.IsValid) return BadRequest();

        List<NewsSiteDto> NewsSitesList = await _dbContext.NewsSite.Select( 
            site => new NewsSiteDto(site.NewSiteName, site.PoliticalLeaning ,site.Siteurl)
        ).ToListAsync();

        return Ok(NewsSitesList);
    }

    [HttpGet("GetSiteByPolitical")]
    public async Task<IActionResult> GetSiteByPolitical(string political)
    {
        if(!ModelState.IsValid) return BadRequest();

        List<NewsSiteDto> NewsSitesList = await _dbContext.NewsSite
        .Where(site => site.PoliticalLeaning == political)
        .Select(site => new NewsSiteDto(site.NewSiteName, site.PoliticalLeaning, site.Siteurl))
        .ToListAsync();

        if(NewsSitesList.IsNullOrEmpty()) return NotFound("We don't have a news site of that political leaning yet");

        return Ok(NewsSitesList);
    }
}


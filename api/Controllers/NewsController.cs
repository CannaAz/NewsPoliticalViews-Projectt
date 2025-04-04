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


    [HttpGet("GetInfoFromNewsSites")]
    public async Task<IActionResult> GetInfoFromNewsSites(string query)
    {
        try
        {
            if(!ModelState.IsValid) return BadRequest();

            List<NewsSiteModel> NewsSitesList = await _dbContext.NewsSite.Where(site => 
                site.IsSearchHttpGetBased == true &&
                site.SiteUrlQueryString != null
            ).ToListAsync();

            List<string> TitlesList = new List<string>();

            foreach(NewsSiteModel NewsSite in NewsSitesList)
            {
                await _driver.Navigate().GoToUrlAsync(NewsSite.SiteUrlQueryString + query);
                var ElementsSearched = _driver.FindElement(By.ClassName(NewsSite.TitleClassName));

                TitlesList.Add($"{NewsSite.NewSiteName}: {ElementsSearched.Text}");
            }

            return Ok(TitlesList);
        }catch(Exception ex)
        {
            Console.WriteLine("An error has ocurred: \n");
            Console.WriteLine(ex.Message);

            return StatusCode(500);
        }
    }

    [HttpGet("GetFullInfo")]
    public async Task<IActionResult> GetFullInfo(string query)
    {
        try
        {
            if(!ModelState.IsValid) return BadRequest();
            /*

            await _driver.Navigate().GoToUrlAsync("https://www.lanacion.com.ar/buscador/?query=adorni");
            IWebElement element = _driver.FindElement(By.ClassName("queryly_item_row"));

            string link = element.FindElement(By.TagName("a")).GetAttribute("href").ToString();
            string title = element.FindElement(By.ClassName("queryly_item_title")).Text;

            return Ok(new string[] {link, title});

            */
            List<NewsSiteModel> AllNewsSites = await _dbContext.NewsSite.Where(site => 
                site.IsSearchHttpGetBased == true &&
                site.ContainerClassName != null &&
                site.TitleClassName != null &&
                site.SiteUrlQueryString != null
            ).ToListAsync();

            List<List<string>> AllInfo = new List<List<string>>();
            
            foreach(NewsSiteModel NewsSite in AllNewsSites)
            {
                await _driver.Navigate().GoToUrlAsync(NewsSite.SiteUrlQueryString + query);
                IWebElement element = _driver.FindElement(By.ClassName(NewsSite.ContainerClassName));

                string title = element.FindElement(By.ClassName(NewsSite.TitleClassName)).Text ?? "Unable to find the title";
                string url = element.FindElement(By.TagName("a")).GetAttribute("href").ToString() ?? "Url Not Found";

                

                List<string> burnerList = new List<string>
                {
                   NewsSite.NewSiteName ,title , url
                };

                AllInfo.Add(burnerList);
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
            site => new NewsSiteDto(site.NewSiteName, site.Siteurl)
        ).ToListAsync();

        return Ok(NewsSitesList);
    }

    [HttpGet("GetSiteByPolitical")]
    public async Task<IActionResult> GetSiteByPolitical(string political)
    {
        if(!ModelState.IsValid) return BadRequest();

        List<NewsSiteDto> NewsSitesList = await _dbContext.NewsSite
        .Where(site => site.PoliticalLeaning == political)
        .Select(site => new NewsSiteDto(site.NewSiteName, site.Siteurl))
        .ToListAsync();

        if(NewsSitesList.IsNullOrEmpty()) return NotFound("We don't have a news site of that political leaning yet");

        return Ok(NewsSitesList);
    }
}


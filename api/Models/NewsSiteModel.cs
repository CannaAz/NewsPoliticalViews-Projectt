namespace api.Models;

public class NewsSiteModel
{
    public Guid Id {get; set;}
    public string NewSiteName {get; set;}
    public string Siteurl {get; set;}
    public string SiteUrlQueryString {get; set;}
    public bool IsSearchHttpGetBased {get; set;}
    public string PoliticalLeaning {get; set;}

    
}

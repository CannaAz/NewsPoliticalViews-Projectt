using System.ComponentModel.DataAnnotations.Schema;

namespace api;

public class NewsSiteSearchInfoModel
{
    public Guid Id {get; set;}

    public string NewsSiteName {get; set;}

    public bool isMainSiteNews {get; set;}

    public string NewsContainerClassName {get; set;}
    public string TitleClassName {get; set;}

    public string? DescriptionClassName {get; set;}

    public string? NewsImageClassName {get; set;}

    
}

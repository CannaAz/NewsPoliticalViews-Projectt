namespace api;

public record NewsInfoDto
(
    string NewsSiteName,
    string NewsTitle,
    string NewsDescription,
    string NewsUrl,
    string NewsImageUrl,
    string NewsSitePolitical
);

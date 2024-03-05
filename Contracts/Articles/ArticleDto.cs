namespace Contracts.Articles;

public class ArticleDto
{
    public required string CreatorName { get; set; }
    public required string CategoryName { get; set; }
    public required string Title { get; set; }
    public string? ContentSummary { get; set; }
    public required string ContentMain { get; set; }
    public DateTime PublishDate { get; set; }
    public string? Picture { get; set; }
    public int? ViewCount { get; set; }
}
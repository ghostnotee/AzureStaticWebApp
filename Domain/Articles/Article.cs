using Domain.Shared;

namespace Domain.Articles;

public class Article : Entity
{
    public Article()
    {
    }

    public Article(Guid creatorId, Guid categoryId, string title, string contentSummary, string contentMain)
    {
        CreatorId = creatorId;
        CategoryId = categoryId;
        Title = title;
        ContentSummary = contentSummary;
        ContentMain = contentMain;
    }

    public Guid CreatorId { get; private set; }
    public Guid CategoryId { get; private set; }
    public string Title { get; private set; } = null!;
    public string ContentSummary { get; private set; } = null!;
    public string ContentMain { get; private set; } = null!;
    public DateTime PublishDate { get; private set; }
    public string? Picture { get; private set;}
    public int ViewCount { get; private set;}

    public void PublishArticle()
    {
        PublishDate = DateTime.UtcNow;
    }
}
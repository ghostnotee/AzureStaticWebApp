using Domain.Shared;

namespace Domain.Articles.Entities;

public class ArticleCategory : Entity
{
    public ArticleCategory()
    {
    }

    public ArticleCategory(Guid? parentId, string name)
    {
        ParentId = parentId;
        Name = name;
    }

    public Guid? ParentId { get; private set; }
    public string Name { get; private set; } = null!;

    public ICollection<Article>? Articles { get; private set; }
    public ICollection<ArticleCategory>? ArticleCategories { get; private set; }

    public void UpdateCategory(Guid? parentId, string name)
    {
        ParentId = parentId;
        Name = name.Trim();
    }
}
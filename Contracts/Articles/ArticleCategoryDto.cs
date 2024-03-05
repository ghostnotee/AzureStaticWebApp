namespace Contracts.Articles;

public class ArticleCategoryDto
{
    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }
    public required string Name { get; set; }
}
using Contracts.Articles;
using Domain.Articles;
using Domain.Articles.Entities;
using Mapster;

namespace Application.Common.Mapping;

public class ArticleMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ArticleCategory, ArticleCategoryDto>();
        config.NewConfig<(Article article, string userName, string categoryName), ArticleDto>()
            .Map(dest=>dest.CategoryName,src=>src.categoryName)
            .Map(dest => dest, src => src.article)
            .Map(dest => dest.CreatorName, src => src.userName);
    }
}
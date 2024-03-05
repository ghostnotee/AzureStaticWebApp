using Application.Common.Interfaces.Persistence;
using Contracts.Articles;
using Domain.Articles.Entities;
using Domain.Shared;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Articles.Queries.ArticleCategories;

public record SearchArticleCategoryQuery(string Name, byte Skip = 0, byte Take = 10) : IRequest<Result<IEnumerable<ArticleCategoryDto>>>;

public class SearchArticleCategoryQueryHandler(ISqlRepository<ArticleCategory> articleCategories, IMapper mapper)
    : IRequestHandler<SearchArticleCategoryQuery, Result<IEnumerable<ArticleCategoryDto>>>
{
    public async Task<Result<IEnumerable<ArticleCategoryDto>>> Handle(SearchArticleCategoryQuery request, CancellationToken cancellationToken)
    {
        var articlecategories = await articleCategories.AsNoTracking()
            .Where(x => x.Name.Contains(request.Name.Trim(), StringComparison.CurrentCultureIgnoreCase)).Skip(request.Skip).Take(request.Take)
            .ToListAsync(cancellationToken);
        return Result.Success(mapper.Map<IEnumerable<ArticleCategoryDto>>(articlecategories));
    }
}
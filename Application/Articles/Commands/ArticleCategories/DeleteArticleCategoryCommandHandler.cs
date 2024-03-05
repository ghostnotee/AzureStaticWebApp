using Application.Common.Interfaces.Persistence;
using Domain.Articles.Entities;
using Domain.Shared;
using MediatR;

namespace Application.Articles.Commands.ArticleCategories;

public record DeleteArticleCategoryCommand(Guid Id) : IRequest<Result<Unit>>;

public class DeleteArticleCategoryCommandHandler(ISqlRepository<ArticleCategory> articleCategories) : IRequestHandler<DeleteArticleCategoryCommand, Result<Unit>>
{
    public Task<Result<Unit>> Handle(DeleteArticleCategoryCommand request, CancellationToken cancellationToken)
    {
        articleCategories.Remove(x=>x.Id==request.Id);
        return Task.FromResult<Result<Unit>>(Unit.Value);
    }
}
using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.Account;

namespace Application.UseCases.Queries;

public class ListAccountsInteractor : IInteractor<Query.ListAccountsDetails, IPagedResult<Projection.AccountDetails>>
{
    private readonly IProjectionGateway<Projection.AccountDetails> _projectionGateway;

    public ListAccountsInteractor(IProjectionGateway<Projection.AccountDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task<IPagedResult<Projection.AccountDetails>?> InteractAsync(Query.ListAccountsDetails query, CancellationToken cancellationToken)
        => _projectionGateway.ListAsync(query.Paging, cancellationToken);
}
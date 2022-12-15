using Application.UseCases.Queries;
using Contracts.Services.Catalog.Protobuf;
using Grpc.Core;

namespace GrpcService;

public class CatalogGrpcService : CatalogService.CatalogServiceBase
{
    private readonly IGetCatalogItemDetailsInteractor _getCatalogItemDetailsInteractor;
    private readonly IListCatalogItemsCardsInteractor _listCatalogItemsCardsInteractor;
    private readonly IListCatalogsGridItemsInteractor _listCatalogsGridItemsInteractor;
    private readonly IListCatalogItemsListItemsInteractor _listCatalogItemsListItemsInteractor;

    public CatalogGrpcService(
        IGetCatalogItemDetailsInteractor getCatalogItemDetailsInteractor,
        IListCatalogItemsCardsInteractor listCatalogItemsCardsInteractor,
        IListCatalogsGridItemsInteractor listCatalogsGridItemsInteractor,
        IListCatalogItemsListItemsInteractor lstCatalogItemsListItemsInteractor)
    {
        _getCatalogItemDetailsInteractor = getCatalogItemDetailsInteractor;
        _listCatalogItemsCardsInteractor = listCatalogItemsCardsInteractor;
        _listCatalogsGridItemsInteractor = listCatalogsGridItemsInteractor;
        _listCatalogItemsListItemsInteractor = lstCatalogItemsListItemsInteractor;
    }

    public override async Task<CatalogItemDetailsResponse> GetCatalogItemDetails(GetCatalogItemDetailsRequest request, ServerCallContext context)
    {
        var itemDetails = await _getCatalogItemDetailsInteractor.InteractAsync(request, context.CancellationToken);
        return itemDetails is null ? new() : new() { CatalogItemDetails = itemDetails };
    }

    public override async Task<CatalogsGridItemsPagedResponse> ListCatalogsGridItems(ListCatalogsGridItemsRequest request, ServerCallContext context)
    {
        var pagedResult = await _listCatalogsGridItemsInteractor.InteractAsync(request, context.CancellationToken);

        return new()
        {
            Items = { pagedResult.Items.Select(gridItem => (CatalogGridItem)gridItem) },
            Page = new()
            {
                Current = pagedResult.Page.Current,
                Size = pagedResult.Page.Size,
                HasNext = pagedResult.Page.HasNext,
                HasPrevious = pagedResult.Page.HasPrevious
            }
        };
    }

    public override async Task<CatalogItemsListItemsPagedResponse> ListCatalogItemsListItems(ListCatalogItemsListItemsRequest request, ServerCallContext context)
    {
        var pagedResult = await _listCatalogItemsListItemsInteractor.InteractAsync(request, context.CancellationToken);

        return new()
        {
            Items = { pagedResult.Items.Select(listItem => (CatalogItemListItem)listItem) },
            Page = new()
            {
                Current = pagedResult.Page.Current,
                Size = pagedResult.Page.Size,
                HasNext = pagedResult.Page.HasNext,
                HasPrevious = pagedResult.Page.HasPrevious
            }
        };
    }

    public override async Task<CatalogItemsCardsPagedResponse> ListCatalogItemsCards(ListCatalogItemsCardsRequest request, ServerCallContext context)
    {
        var pagedResult = await _listCatalogItemsCardsInteractor.InteractAsync(request, context.CancellationToken);

        return new()
        {
            Items = { pagedResult.Items.Select(card => (CatalogItemCard)card) },
            Page = new()
            {
                Current = pagedResult.Page.Current,
                Size = pagedResult.Page.Size,
                HasNext = pagedResult.Page.HasNext,
                HasPrevious = pagedResult.Page.HasPrevious
            }
        };
    }
}
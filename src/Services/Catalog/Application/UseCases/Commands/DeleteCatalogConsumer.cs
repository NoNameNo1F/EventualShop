using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using DeleteCatalogCommand = Messages.Catalogs.Commands.DeleteCatalog;

namespace Application.UseCases.Commands
{
    public class DeleteCatalogConsumer : IConsumer<DeleteCatalogCommand>
    {
        private readonly ICatalogEventStoreService _eventStoreService;

        public DeleteCatalogConsumer(ICatalogEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<DeleteCatalogCommand> context)
        {
            var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CatalogId, context.CancellationToken);
            catalog.Delete(context.Message.CatalogId);
            await _eventStoreService.AppendEventsToStreamAsync(catalog, context.CancellationToken);
        }
    }
}
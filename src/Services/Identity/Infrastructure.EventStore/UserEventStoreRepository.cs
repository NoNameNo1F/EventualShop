using Application.EventStore;
using Domain;
using Domain.Aggregates;
using Infrastructure.EventStore.Abstractions;
using Infrastructure.EventStore.Contexts;

namespace Infrastructure.EventStore;

public class UserEventStoreRepository : EventStoreRepository<User, StoreEvents.Event, StoreEvents.Snapshot, Guid>, IUserEventStoreRepository
{
    public UserEventStoreRepository(EventStoreDbContext dbContext)
        : base(dbContext) { }
}
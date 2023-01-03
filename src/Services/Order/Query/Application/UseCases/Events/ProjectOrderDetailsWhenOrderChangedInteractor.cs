﻿using Application.Abstractions;
using Contracts.Services.Order;

namespace Application.UseCases.Events;

public interface IProjectOrderDetailsWhenOrderChangedInteractor :
    IInteractor<DomainEvent.OrderPlaced>,
    IInteractor<DomainEvent.OrderConfirmed> { }

public class ProjectOrderDetailsWhenOrderChangedInteractor : IProjectOrderDetailsWhenOrderChangedInteractor
{
    private readonly IProjectionGateway<Projection.OrderDetails> _projectionGateway;

    public ProjectOrderDetailsWhenOrderChangedInteractor(IProjectionGateway<Projection.OrderDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.OrderPlaced @event, CancellationToken cancellationToken)
    {
        Projection.OrderDetails orderDetails =
            new(@event.OrderId,
                @event.CustomerId,
                @event.Total,
                @event.BillingAddress,
                @event.ShippingAddress,
                @event.Items,
                @event.PaymentMethods,
                @event.Status,
                false);

        await _projectionGateway.InsertAsync(orderDetails, cancellationToken);
    }

    public Task InteractAsync(DomainEvent.OrderConfirmed @event, CancellationToken cancellationToken)
        => _projectionGateway.UpdateFieldAsync(@event.OrderId, order => order.Status, @event.Status, cancellationToken);
}
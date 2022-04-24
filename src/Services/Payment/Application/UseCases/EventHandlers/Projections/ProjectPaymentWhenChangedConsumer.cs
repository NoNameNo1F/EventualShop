﻿using Application.Abstractions.Projections;
using Domain.Enumerations;
using ECommerce.Contracts.Payments;
using MassTransit;

namespace Application.UseCases.EventHandlers.Projections;

public class ProjectPaymentWhenChangedConsumer :
    IConsumer<DomainEvent.PaymentCanceled>,
    IConsumer<DomainEvent.PaymentRequested>
{
    private readonly IProjectionRepository<ECommerce.Contracts.Payments.Projection.Payment> _repository;

    public ProjectPaymentWhenChangedConsumer(IProjectionRepository<ECommerce.Contracts.Payments.Projection.Payment> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<DomainEvent.PaymentCanceled> context)
        => await _repository.UpdateFieldAsync(
            id: context.Message.PaymentId,
            field: payment => payment.Status,
            value: PaymentStatus.Canceled.ToString(),
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.PaymentRequested> context)
    {
        var payment = new ECommerce.Contracts.Payments.Projection.Payment
        {
            Amount = context.Message.Amount,
            Id = context.Message.PaymentId,
            Status = context.Message.Status,
            IsDeleted = false,
            OrderId = context.Message.OrderId
        };

        await _repository.InsertAsync(payment, context.CancellationToken);
    }
}
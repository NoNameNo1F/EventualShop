﻿using Application.Abstractions.Projections;
using ECommerce.Contracts.Accounts;
using MassTransit;

namespace Application.UseCases.EventHandlers.Projections;

public class ProjectAccountDetailsWhenChangedConsumer :
    IConsumer<DomainEvent.AccountCreated>,
    IConsumer<DomainEvent.AccountDeleted>,
    IConsumer<DomainEvent.ProfessionalAddressDefined>,
    IConsumer<DomainEvent.ProfileUpdated>,
    IConsumer<DomainEvent.ResidenceAddressDefined>
{
    private readonly IProjectionRepository<Projection.Account> _projectionRepository;

    public ProjectAccountDetailsWhenChangedConsumer(IProjectionRepository<Projection.Account> projectionRepository)
    {
        _projectionRepository = projectionRepository;
    }

    public async Task Consume(ConsumeContext<DomainEvent.AccountCreated> context)
    {
        var account = new Projection.Account
        {
            Id = context.Message.AccountId,
            UserId = context.Message.UserId,
            Profile = new()
            {
                Email = context.Message.Email,
                FirstName = context.Message.FirstName,
            },
            IsDeleted = false
        };

        await _projectionRepository.InsertAsync(account, context.CancellationToken);
    }

    public async Task Consume(ConsumeContext<DomainEvent.AccountDeleted> context)
        => await _projectionRepository.DeleteAsync(context.Message.AccountId, context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.ProfessionalAddressDefined> context)
        => await _projectionRepository.UpdateFieldAsync(
            id: context.Message.AccountId,
            field: account => account.Profile.ProfessionalAddress,
            value: context.Message.Address,
            cancellationToken: context.CancellationToken);

    // TODO - Improve this, update all profile dont like the right approach
    public async Task Consume(ConsumeContext<DomainEvent.ProfileUpdated> context)
        => await _projectionRepository.UpdateFieldAsync(
            id: context.Message.AccountId,
            field: account => account.Profile,
            value: new()
            {
                Birthdate = context.Message.Birthdate,
                Email = context.Message.Email,
                FirstName = context.Message.FirstName,
                LastName = context.Message.LastName
            },
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.ResidenceAddressDefined> context)
        => await _projectionRepository.UpdateFieldAsync(
            id: context.Message.AccountId,
            field: account => account.Profile.ResidenceAddress,
            value: context.Message.Address,
            cancellationToken: context.CancellationToken);
}
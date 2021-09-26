﻿using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using Messages.Identities.Commands;

namespace Application.UseCases.Commands
{
    public class ChangeUserPasswordConsumer : IConsumer<ChangeUserPassword>
    {
        private readonly IUserEventStoreService _eventStoreService;

        public ChangeUserPasswordConsumer(IUserEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<ChangeUserPassword> context)
        {
            var user = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.UserId, context.CancellationToken);

            if (user is null)
            {
                // TODO - Notification
                return;
            }

            user.ChangePassword(
                user.Id,
                context.Message.NewPassword,
                context.Message.NewPasswordConfirmation);

            await _eventStoreService.AppendEventsToStreamAsync(user, context.CancellationToken);
        }
    }
}
﻿using System;
using Application.UseCases.Events.Integrations;
using Application.UseCases.Events.Projections;
using GreenPipes;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Messages.Abstractions.Events;
using Messages.Services.ShoppingCarts;
using DomainEvents = Messages.Services.Orders.DomainEvents;

namespace Infrastructure.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistration registration)
    {
        cfg.ConfigureEventReceiveEndpoint<CartSubmittedConsumer, IntegrationEvents.CartSubmitted>(registration);
        cfg.ConfigureEventReceiveEndpoint<OrderPlacedConsumer, DomainEvents.OrderPlaced>(registration);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TMessage>(this IRabbitMqBusFactoryConfigurator bus, IRegistration registration)
        where TConsumer : class, IConsumer
        where TMessage : class, IEvent
    {
        bus.ReceiveEndpoint(
            queueName: $"order-{typeof(TMessage).ToKebabCaseString()}",
            configureEndpoint: endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;

                endpoint.ConfigureConsumer<TConsumer>(registration);
                endpoint.Bind<TMessage>();

                endpoint.UseCircuitBreaker(circuitBreaker => // TODO - Options
                {
                    circuitBreaker.TripThreshold = 15;
                    circuitBreaker.ResetInterval = TimeSpan.FromMinutes(3);
                    circuitBreaker.TrackingPeriod = TimeSpan.FromMinutes(1);
                    circuitBreaker.ActiveThreshold = 10;
                });

                endpoint.UseRateLimit(100, TimeSpan.FromSeconds(1)); // TODO - Options
            });
    }
}
﻿using System;
using Application.UseCases.Commands;
using Application.UseCases.Events.Integrations;
using Application.UseCases.Events.Projections;
using MassTransit;
using Messages.Abstractions;
using Messages.Services.ShoppingCarts;

namespace Infrastructure.DependencyInjection.Extensions;

public static class RegistrationConfiguratorExtensions
{
    public static void AddCommandConsumers(this IRegistrationConfigurator cfg)
    {
        cfg.AddCommandConsumer<PlaceOrderConsumer, Commands.CreateCart>();
    }

    public static void AddEventConsumers(this IRegistrationConfigurator cfg)
    {
        cfg.AddConsumer<OrderPlacedConsumer>();
        cfg.AddConsumer<CartSubmittedConsumer>();
    }

    public static void AddQueryConsumers(this IRegistrationConfigurator cfg)
    {
        // cfg.AddConsumer<GetUserAuthenticationDetailsConsumer>();
    }

    private static void AddCommandConsumer<TConsumer, TMessage>(this IRegistrationConfigurator configurator)
        where TConsumer : class, IConsumer
        where TMessage : class, IMessage
    {
        configurator
            .AddConsumer<TConsumer>()
            .Endpoint(endpoint => endpoint.ConfigureConsumeTopology = false);

        EndpointConvention.Map<TMessage>(new Uri($"exchange:{typeof(TMessage).ToKebabCaseString()}"));
    }
}
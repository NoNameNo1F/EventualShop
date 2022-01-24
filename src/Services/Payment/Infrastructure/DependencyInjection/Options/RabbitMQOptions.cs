﻿using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DependencyInjection.Options;

public record RabbitMqOptions
{
    [Required, MinLength(5)]
    public string Host { get; init; }

    [Required, MinLength(5)]
    public string Username { get; init; }

    [Required, MinLength(5)]
    public string Password { get; init; }

    [Required]
    public string VirtualHost { get; init; }

    [Required]
    public ushort Port { get; init; }

    [Required, MinLength(5)]
    public string SchedulerQueueName { get; init; }

    [Required, Range(1, 10)]
    public int RetryLimit { get; init; }

    [Required, Range(1, 10)]
    public int MinimumInterval { get; init; }

    [Required, Range(2, 100)]
    public int MaximumInterval { get; init; }

    [Required, Range(0, 10)]
    public int IntervalDelta { get; init; }
}
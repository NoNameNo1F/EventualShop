﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DependencyInjection.Options
{
    public record SqlServerRetryingOptions
    {
        [Required, Range(5, 20)]
        public int MaxRetryCount { get; init; }

        [Required, Range(5, 20)]
        public int MaxSecondsRetryDelay { get; init; }

        public int[] ErrorNumbersToAdd { get; init; }

        internal TimeSpan MaxRetryDelay
            => TimeSpan.FromSeconds(MaxSecondsRetryDelay);
    }
}
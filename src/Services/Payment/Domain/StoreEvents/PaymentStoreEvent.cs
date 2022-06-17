﻿using Domain.Abstractions.StoreEvents;
using Domain.Aggregates;

namespace Domain.StoreEvents;

public record PaymentStoreEvent : StoreEvent<Payment, Guid>;

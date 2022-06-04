﻿using Contracts.Abstractions;

namespace Contracts.Services.Payment;

public static class Query
{
    public record GetPayment(Guid PaymentId) : Message(CorrelationId: PaymentId), IQuery;
}
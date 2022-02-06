﻿using System;
using MassTransit.Topology;

namespace ECommerce.Abstractions.Messages.Queries;

[ExcludeFromTopology]
public abstract record Query(Guid CorrelationId = default) : Message(CorrelationId), IQuery;
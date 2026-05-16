// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Common.Interfaces.Services;
using CleanArchitecture.Blazor.Application.Features.VisitorHistories.DTOs;
using CleanArchitecture.Blazor.Application.Features.VisitorHistories.Queries.GetAll;

namespace CleanArchitecture.Blazor.Application.Services.Features;

public class VisitorHistoryService : IVisitorHistoryService
{
    private readonly ISender _mediator;

    public VisitorHistoryService(ISender mediator)
    {
        _mediator = mediator;
    }

    public async Task<List<VisitorHistoryDto>> GetByVisitorIdAsync(int? visitorId, CancellationToken cancellationToken = default)
        => (await _mediator.Send(new GetByVisitorIdVisitorHistoriesQuery(visitorId), cancellationToken)).ToList();
}

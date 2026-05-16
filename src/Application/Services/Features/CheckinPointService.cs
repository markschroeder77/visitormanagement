// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Common.Interfaces.Services;
using CleanArchitecture.Blazor.Application.Features.CheckinPoints.DTOs;
using CleanArchitecture.Blazor.Application.Features.CheckinPoints.Queries.GetAll;

namespace CleanArchitecture.Blazor.Application.Services.Features;

public class CheckinPointService : ICheckinPointService
{
    private readonly ISender _mediator;

    public CheckinPointService(ISender mediator)
    {
        _mediator = mediator;
    }

    public async Task<List<CheckinPointDto>> GetAllCheckinPointsAsync(CancellationToken cancellationToken = default)
        => (await _mediator.Send(new GetAllCheckinPointsQuery(), cancellationToken)).ToList();
}
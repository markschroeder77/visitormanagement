// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Common.Interfaces.Services;
using CleanArchitecture.Blazor.Application.Features.Designations.DTOs;
using CleanArchitecture.Blazor.Application.Features.Designations.Queries.GetAll;

namespace CleanArchitecture.Blazor.Application.Services.Features;

public class DesignationService : IDesignationService
{
    private readonly ISender _mediator;

    public DesignationService(ISender mediator)
    {
        _mediator = mediator;
    }

    public async Task<List<DesignationDto>> GetAllDesignationsAsync(CancellationToken cancellationToken = default)
        => (await _mediator.Send(new GetAllDesignationsQuery(), cancellationToken)).ToList();
}
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Common.Interfaces.Services;
using CleanArchitecture.Blazor.Application.Features.Sites.DTOs;
using CleanArchitecture.Blazor.Application.Features.Sites.Queries.GetAll;

namespace CleanArchitecture.Blazor.Application.Services.Features;

public class SiteService : ISiteService
{
    private readonly ISender _mediator;

    public SiteService(ISender mediator)
    {
        _mediator = mediator;
    }

    public async Task<List<SiteDto>> GetAllSitesAsync(CancellationToken cancellationToken = default)
        => (await _mediator.Send(new GetAllSitesQuery(), cancellationToken)).ToList();
}

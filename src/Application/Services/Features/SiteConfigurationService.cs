// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Common.Interfaces.Services;
using CleanArchitecture.Blazor.Application.Features.SiteConfigurations.DTOs;
using CleanArchitecture.Blazor.Application.Features.SiteConfigurations.Queries.GetAll;

namespace CleanArchitecture.Blazor.Application.Services.Features;

public class SiteConfigurationService : ISiteConfigurationService
{
    private readonly ISender _mediator;

    public SiteConfigurationService(ISender mediator)
    {
        _mediator = mediator;
    }

    public async Task<SiteConfigurationDto?> GetBySiteIdAsync(int siteId, CancellationToken cancellationToken = default)
        => await _mediator.Send(new GetBySiteIdConfigurationsQuery(siteId), cancellationToken);
}

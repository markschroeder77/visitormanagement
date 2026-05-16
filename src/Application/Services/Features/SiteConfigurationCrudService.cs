// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Common.Interfaces.Services;
using CleanArchitecture.Blazor.Application.Common.Models;
using CleanArchitecture.Blazor.Application.Features.SiteConfigurations.DTOs;
using CleanArchitecture.Blazor.Application.Features.SiteConfigurations.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.SiteConfigurations.Commands.Delete;
using CleanArchitecture.Blazor.Application.Features.SiteConfigurations.Queries.Pagination;
using CleanArchitecture.Blazor.Application.Features.SiteConfigurations.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.SiteConfigurations.Commands.Delete;

namespace CleanArchitecture.Blazor.Application.Services.Features;

public class SiteConfigurationCrudService : ISiteConfigurationCrudService
{
    private readonly ISender _mediator;
    public SiteConfigurationCrudService(ISender mediator) => _mediator = mediator;

    public async Task<PaginatedData<SiteConfigurationDto>> GetSiteConfigurationsWithPaginationAsync(SiteConfigurationsWithPaginationQuery query, CancellationToken ct = default)
        => await _mediator.Send(query, ct);

    public async Task<Result<int>> SaveSiteConfigurationAsync(AddEditSiteConfigurationCommand command, CancellationToken ct = default)
        => await _mediator.Send(command, ct);

    public async Task<Result> DeleteSiteConfigurationsAsync(DeleteSiteConfigurationCommand command, CancellationToken ct = default)
        => await _mediator.Send(command, ct);
}

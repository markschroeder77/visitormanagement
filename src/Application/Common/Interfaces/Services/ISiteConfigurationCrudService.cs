// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.SiteConfigurations.DTOs;
using CleanArchitecture.Blazor.Application.Common.Models;
using CleanArchitecture.Blazor.Application.Features.SiteConfigurations.Queries.Pagination;
using CleanArchitecture.Blazor.Application.Features.SiteConfigurations.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.SiteConfigurations.Commands.Delete;

namespace CleanArchitecture.Blazor.Application.Common.Interfaces.Services;

public interface ISiteConfigurationCrudService
{
    Task<PaginatedData<SiteConfigurationDto>> GetSiteConfigurationsWithPaginationAsync(SiteConfigurationsWithPaginationQuery query, CancellationToken cancellationToken = default);
    Task<Result<int>> SaveSiteConfigurationAsync(AddEditSiteConfigurationCommand command, CancellationToken cancellationToken = default);
    Task<Result> DeleteSiteConfigurationsAsync(DeleteSiteConfigurationCommand command, CancellationToken cancellationToken = default);
}

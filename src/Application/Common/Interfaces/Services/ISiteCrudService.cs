// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Sites.DTOs;
using CleanArchitecture.Blazor.Application.Common.Models;
using CleanArchitecture.Blazor.Application.Features.Sites.Queries.Pagination;
using CleanArchitecture.Blazor.Application.Features.Sites.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.Sites.Commands.Delete;

namespace CleanArchitecture.Blazor.Application.Common.Interfaces.Services;

public interface ISiteCrudService
{
    Task<PaginatedData<SiteDto>> GetSitesWithPaginationAsync(SitesWithPaginationQuery query, CancellationToken cancellationToken = default);
    Task<Result<int>> SaveSiteAsync(AddEditSiteCommand command, CancellationToken cancellationToken = default);
    Task<Result> DeleteSitesAsync(DeleteSiteCommand command, CancellationToken cancellationToken = default);
}

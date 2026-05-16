// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Common.Interfaces.Services;
using CleanArchitecture.Blazor.Application.Common.Models;
using CleanArchitecture.Blazor.Application.Features.Sites.DTOs;
using CleanArchitecture.Blazor.Application.Features.Sites.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.Sites.Commands.Delete;
using CleanArchitecture.Blazor.Application.Features.Sites.Queries.Pagination;
using CleanArchitecture.Blazor.Application.Features.Sites.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.Sites.Commands.Delete;

namespace CleanArchitecture.Blazor.Application.Services.Features;

public class SiteCrudService : ISiteCrudService
{
    private readonly ISender _mediator;
    public SiteCrudService(ISender mediator) => _mediator = mediator;

    public async Task<PaginatedData<SiteDto>> GetSitesWithPaginationAsync(SitesWithPaginationQuery query, CancellationToken ct = default)
        => await _mediator.Send(query, ct);

    public async Task<Result<int>> SaveSiteAsync(AddEditSiteCommand command, CancellationToken ct = default)
        => await _mediator.Send(command, ct);

    public async Task<Result> DeleteSitesAsync(DeleteSiteCommand command, CancellationToken ct = default)
        => await _mediator.Send(command, ct);
}

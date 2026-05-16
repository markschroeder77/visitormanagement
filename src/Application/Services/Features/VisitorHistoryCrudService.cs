// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Common.Interfaces.Services;
using CleanArchitecture.Blazor.Application.Common.Models;
using CleanArchitecture.Blazor.Application.Features.VisitorHistories.DTOs;
using CleanArchitecture.Blazor.Application.Features.VisitorHistories.Queries.Pagination;
using CleanArchitecture.Blazor.Application.Features.VisitorHistories.Commands.Delete;

namespace CleanArchitecture.Blazor.Application.Services.Features;

public class VisitorHistoryCrudService : IVisitorHistoryCrudService
{
    private readonly ISender _mediator;
    public VisitorHistoryCrudService(ISender mediator) => _mediator = mediator;

    public async Task<PaginatedData<VisitorHistoryDto>> GetVisitorHistoriesWithPaginationAsync(VisitorHistoriesWithPaginationQuery query, CancellationToken ct = default)
        => await _mediator.Send(query, ct);

    public async Task<Result> DeleteVisitorHistoriesAsync(int[] ids, CancellationToken ct = default)
        => await _mediator.Send(new DeleteVisitorHistoryCommand(ids), ct);
}

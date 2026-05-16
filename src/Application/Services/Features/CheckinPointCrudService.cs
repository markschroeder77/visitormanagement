// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Common.Interfaces.Services;
using CleanArchitecture.Blazor.Application.Common.Models;
using CleanArchitecture.Blazor.Application.Features.CheckinPoints.DTOs;
using CleanArchitecture.Blazor.Application.Features.CheckinPoints.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.CheckinPoints.Commands.Delete;
using CleanArchitecture.Blazor.Application.Features.CheckinPoints.Queries.Pagination;
using CleanArchitecture.Blazor.Application.Features.CheckinPoints.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.CheckinPoints.Commands.Delete;

namespace CleanArchitecture.Blazor.Application.Services.Features;

public class CheckinPointCrudService : ICheckinPointCrudService
{
    private readonly ISender _mediator;
    public CheckinPointCrudService(ISender mediator) => _mediator = mediator;

    public async Task<PaginatedData<CheckinPointDto>> GetCheckinPointsWithPaginationAsync(CheckinPointsWithPaginationQuery query, CancellationToken ct = default)
        => await _mediator.Send(query, ct);

    public async Task<Result<int>> SaveCheckinPointAsync(AddEditCheckinPointCommand command, CancellationToken ct = default)
        => await _mediator.Send(command, ct);

    public async Task<Result> DeleteCheckinPointsAsync(DeleteCheckinPointCommand command, CancellationToken ct = default)
        => await _mediator.Send(command, ct);
}

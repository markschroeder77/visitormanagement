// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Common.Interfaces.Services;
using CleanArchitecture.Blazor.Application.Common.Models;
using CleanArchitecture.Blazor.Application.Features.Designations.DTOs;
using CleanArchitecture.Blazor.Application.Features.Designations.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.Designations.Commands.Delete;
using CleanArchitecture.Blazor.Application.Features.Designations.Queries.Pagination;
using CleanArchitecture.Blazor.Application.Features.Designations.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.Designations.Commands.Delete;

namespace CleanArchitecture.Blazor.Application.Services.Features;

public class DesignationCrudService : IDesignationCrudService
{
    private readonly ISender _mediator;
    public DesignationCrudService(ISender mediator) => _mediator = mediator;

    public async Task<PaginatedData<DesignationDto>> GetDesignationsWithPaginationAsync(DesignationsWithPaginationQuery query, CancellationToken ct = default)
        => await _mediator.Send(query, ct);

    public async Task<Result<int>> SaveDesignationAsync(AddEditDesignationCommand command, CancellationToken ct = default)
        => await _mediator.Send(command, ct);

    public async Task<Result> DeleteDesignationsAsync(DeleteDesignationCommand command, CancellationToken ct = default)
        => await _mediator.Send(command, ct);
}

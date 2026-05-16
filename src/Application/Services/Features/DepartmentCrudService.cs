// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Common.Interfaces.Services;
using CleanArchitecture.Blazor.Application.Common.Models;
using CleanArchitecture.Blazor.Application.Features.Departments.DTOs;
using CleanArchitecture.Blazor.Application.Features.Departments.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.Departments.Commands.Delete;
using CleanArchitecture.Blazor.Application.Features.Departments.Queries.Pagination;
using CleanArchitecture.Blazor.Application.Features.Departments.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.Departments.Commands.Delete;

namespace CleanArchitecture.Blazor.Application.Services.Features;

public class DepartmentCrudService : IDepartmentCrudService
{
    private readonly ISender _mediator;
    public DepartmentCrudService(ISender mediator) => _mediator = mediator;

    public async Task<PaginatedData<DepartmentDto>> GetDepartmentsWithPaginationAsync(DepartmentsWithPaginationQuery query, CancellationToken ct = default)
        => await _mediator.Send(query, ct);

    public async Task<Result<int>> SaveDepartmentAsync(AddEditDepartmentCommand command, CancellationToken ct = default)
        => await _mediator.Send(command, ct);

    public async Task<Result> DeleteDepartmentsAsync(DeleteDepartmentCommand command, CancellationToken ct = default)
        => await _mediator.Send(command, ct);
}

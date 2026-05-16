// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Common.Interfaces.Services;
using CleanArchitecture.Blazor.Application.Common.Models;
using CleanArchitecture.Blazor.Application.Features.Employees.DTOs;
using CleanArchitecture.Blazor.Application.Features.Employees.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.Employees.Commands.Delete;
using CleanArchitecture.Blazor.Application.Features.Employees.Queries.Pagination;
using CleanArchitecture.Blazor.Application.Features.Employees.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.Employees.Commands.Delete;

namespace CleanArchitecture.Blazor.Application.Services.Features;

public class EmployeeCrudService : IEmployeeCrudService
{
    private readonly ISender _mediator;
    public EmployeeCrudService(ISender mediator) => _mediator = mediator;

    public async Task<PaginatedData<EmployeeDto>> GetEmployeesWithPaginationAsync(EmployeesWithPaginationQuery query, CancellationToken ct = default)
        => await _mediator.Send(query, ct);

    public async Task<Result<int>> SaveEmployeeAsync(AddEditEmployeeCommand command, CancellationToken ct = default)
        => await _mediator.Send(command, ct);

    public async Task<Result> DeleteEmployeesAsync(DeleteEmployeeCommand command, CancellationToken ct = default)
        => await _mediator.Send(command, ct);
}

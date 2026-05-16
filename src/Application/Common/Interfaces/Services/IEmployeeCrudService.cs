// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Employees.DTOs;
using CleanArchitecture.Blazor.Application.Common.Models;
using CleanArchitecture.Blazor.Application.Features.Employees.Queries.Pagination;
using CleanArchitecture.Blazor.Application.Features.Employees.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.Employees.Commands.Delete;

namespace CleanArchitecture.Blazor.Application.Common.Interfaces.Services;

public interface IEmployeeCrudService
{
    Task<PaginatedData<EmployeeDto>> GetEmployeesWithPaginationAsync(EmployeesWithPaginationQuery query, CancellationToken cancellationToken = default);
    Task<Result<int>> SaveEmployeeAsync(AddEditEmployeeCommand command, CancellationToken cancellationToken = default);
    Task<Result> DeleteEmployeesAsync(DeleteEmployeeCommand command, CancellationToken cancellationToken = default);
}

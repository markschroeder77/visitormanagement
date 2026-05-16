// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Departments.DTOs;
using CleanArchitecture.Blazor.Application.Common.Models;
using CleanArchitecture.Blazor.Application.Features.Departments.Queries.Pagination;
using CleanArchitecture.Blazor.Application.Features.Departments.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.Departments.Commands.Delete;

namespace CleanArchitecture.Blazor.Application.Common.Interfaces.Services;

public interface IDepartmentCrudService
{
    Task<PaginatedData<DepartmentDto>> GetDepartmentsWithPaginationAsync(DepartmentsWithPaginationQuery query, CancellationToken cancellationToken = default);
    Task<Result<int>> SaveDepartmentAsync(AddEditDepartmentCommand command, CancellationToken cancellationToken = default);
    Task<Result> DeleteDepartmentsAsync(DeleteDepartmentCommand command, CancellationToken cancellationToken = default);
}

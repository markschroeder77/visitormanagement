// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Designations.DTOs;
using CleanArchitecture.Blazor.Application.Common.Models;
using CleanArchitecture.Blazor.Application.Features.Designations.Queries.Pagination;
using CleanArchitecture.Blazor.Application.Features.Designations.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.Designations.Commands.Delete;

namespace CleanArchitecture.Blazor.Application.Common.Interfaces.Services;

public interface IDesignationCrudService
{
    Task<PaginatedData<DesignationDto>> GetDesignationsWithPaginationAsync(DesignationsWithPaginationQuery query, CancellationToken cancellationToken = default);
    Task<Result<int>> SaveDesignationAsync(AddEditDesignationCommand command, CancellationToken cancellationToken = default);
    Task<Result> DeleteDesignationsAsync(DeleteDesignationCommand command, CancellationToken cancellationToken = default);
}

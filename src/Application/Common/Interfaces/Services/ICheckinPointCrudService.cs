// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.CheckinPoints.DTOs;
using CleanArchitecture.Blazor.Application.Common.Models;
using CleanArchitecture.Blazor.Application.Features.CheckinPoints.Queries.Pagination;
using CleanArchitecture.Blazor.Application.Features.CheckinPoints.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.CheckinPoints.Commands.Delete;

namespace CleanArchitecture.Blazor.Application.Common.Interfaces.Services;

public interface ICheckinPointCrudService
{
    Task<PaginatedData<CheckinPointDto>> GetCheckinPointsWithPaginationAsync(CheckinPointsWithPaginationQuery query, CancellationToken cancellationToken = default);
    Task<Result<int>> SaveCheckinPointAsync(AddEditCheckinPointCommand command, CancellationToken cancellationToken = default);
    Task<Result> DeleteCheckinPointsAsync(DeleteCheckinPointCommand command, CancellationToken cancellationToken = default);
}

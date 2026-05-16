// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Common.Models;
using CleanArchitecture.Blazor.Application.Features.VisitorHistories.DTOs;
using CleanArchitecture.Blazor.Application.Features.VisitorHistories.Queries.Pagination;

namespace CleanArchitecture.Blazor.Application.Common.Interfaces.Services;

public interface IVisitorHistoryCrudService
{
    Task<PaginatedData<VisitorHistoryDto>> GetVisitorHistoriesWithPaginationAsync(VisitorHistoriesWithPaginationQuery query, CancellationToken cancellationToken = default);
    Task<Result> DeleteVisitorHistoriesAsync(int[] ids, CancellationToken cancellationToken = default);
}

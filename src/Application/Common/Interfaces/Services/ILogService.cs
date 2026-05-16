// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Common.Models;
using CleanArchitecture.Blazor.Application.Features.Logs.DTOs;
using CleanArchitecture.Blazor.Application.Features.Logs.Queries.ChatData;
using CleanArchitecture.Blazor.Application.Logs.Queries.PaginationQuery;

namespace CleanArchitecture.Blazor.Application.Common.Interfaces.Services;

public interface ILogService
{
    Task<PaginatedData<LogDto>> GetLogsWithPaginationAsync(LogsWithPaginationQuery query, CancellationToken cancellationToken = default);
    Task<List<LogTimeLineDto>> GetLogsTimeLineChatDataAsync(CancellationToken cancellationToken = default);
}

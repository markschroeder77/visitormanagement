// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Common.Interfaces.Services;
using CleanArchitecture.Blazor.Application.Common.Models;
using CleanArchitecture.Blazor.Application.Features.Logs.DTOs;
using CleanArchitecture.Blazor.Application.Features.Logs.Queries.ChatData;
using CleanArchitecture.Blazor.Application.Logs.Queries.PaginationQuery;

namespace CleanArchitecture.Blazor.Application.Services.Features;

public class LogService : ILogService
{
    private readonly ISender _mediator;
    public LogService(ISender mediator) => _mediator = mediator;

    public async Task<PaginatedData<LogDto>> GetLogsWithPaginationAsync(LogsWithPaginationQuery query, CancellationToken ct = default)
        => await _mediator.Send(query, ct);

    public async Task<List<LogTimeLineDto>> GetLogsTimeLineChatDataAsync(CancellationToken ct = default)
        => await _mediator.Send(new LogsTimeLineChatDataQuery(), ct);
}

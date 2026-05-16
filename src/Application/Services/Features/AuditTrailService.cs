// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Common.Interfaces.Services;
using CleanArchitecture.Blazor.Application.Common.Models;
using CleanArchitecture.Blazor.Application.Features.AuditTrails.DTOs;
using CleanArchitecture.Blazor.Application.AuditTrails.Queries.PaginationQuery;

namespace CleanArchitecture.Blazor.Application.Services.Features;

public class AuditTrailService : IAuditTrailService
{
    private readonly ISender _mediator;
    public AuditTrailService(ISender mediator) => _mediator = mediator;

    public async Task<PaginatedData<AuditTrailDto>> GetAuditTrailsWithPaginationAsync(AuditTrailsWithPaginationQuery query, CancellationToken ct = default)
        => await _mediator.Send(query, ct);
}

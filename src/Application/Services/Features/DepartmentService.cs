// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Common.Interfaces.Services;
using CleanArchitecture.Blazor.Application.Features.Departments.DTOs;
using CleanArchitecture.Blazor.Application.Features.Departments.Queries.GetAll;

namespace CleanArchitecture.Blazor.Application.Services.Features;

public class DepartmentService : IDepartmentService
{
    private readonly ISender _mediator;

    public DepartmentService(ISender mediator)
    {
        _mediator = mediator;
    }

    public async Task<List<DepartmentDto>> GetAllDepartmentsAsync(CancellationToken cancellationToken = default)
        => (await _mediator.Send(new GetAllDepartmentsQuery(), cancellationToken)).ToList();
}
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Common.Interfaces.Services;
using CleanArchitecture.Blazor.Application.Features.Employees.DTOs;
using CleanArchitecture.Blazor.Application.Features.Employees.Queries.GetAll;

namespace CleanArchitecture.Blazor.Application.Services.Features;

public class EmployeeService : IEmployeeService
{
    private readonly ISender _mediator;

    public EmployeeService(ISender mediator)
    {
        _mediator = mediator;
    }

    public async Task<List<EmployeeDto>> GetAllEmployeesAsync(CancellationToken cancellationToken = default)
        => (await _mediator.Send(new GetAllEmployeesQuery(), cancellationToken)).ToList();
}

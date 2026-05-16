// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Common.Interfaces.Services;
using CleanArchitecture.Blazor.Application.Common.Models;
using CleanArchitecture.Blazor.Application.Features.Devices.DTOs;
using CleanArchitecture.Blazor.Application.Features.Devices.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.Devices.Commands.Delete;
using CleanArchitecture.Blazor.Application.Features.Devices.Queries.Pagination;
using CleanArchitecture.Blazor.Application.Features.Devices.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.Devices.Commands.Delete;

namespace CleanArchitecture.Blazor.Application.Services.Features;

public class DeviceCrudService : IDeviceCrudService
{
    private readonly ISender _mediator;
    public DeviceCrudService(ISender mediator) => _mediator = mediator;

    public async Task<PaginatedData<DeviceDto>> GetDevicesWithPaginationAsync(DevicesWithPaginationQuery query, CancellationToken ct = default)
        => await _mediator.Send(query, ct);

    public async Task<Result<int>> SaveDeviceAsync(AddEditDeviceCommand command, CancellationToken ct = default)
        => await _mediator.Send(command, ct);

    public async Task<Result> DeleteDevicesAsync(DeleteDeviceCommand command, CancellationToken ct = default)
        => await _mediator.Send(command, ct);
}

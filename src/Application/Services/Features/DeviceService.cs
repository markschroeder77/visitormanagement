// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Common.Interfaces.Services;
using CleanArchitecture.Blazor.Application.Features.Devices.DTOs;
using CleanArchitecture.Blazor.Application.Features.Devices.Queries.GetAll;

namespace CleanArchitecture.Blazor.Application.Services.Features;

public class DeviceService : IDeviceService
{
    private readonly ISender _mediator;

    public DeviceService(ISender mediator)
    {
        _mediator = mediator;
    }

    public async Task<List<DeviceDto>> GetAllDevicesAsync(CancellationToken cancellationToken = default)
        => (await _mediator.Send(new GetAllDevicesQuery(), cancellationToken)).ToList();
}
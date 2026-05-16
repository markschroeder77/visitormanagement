// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Devices.DTOs;
using CleanArchitecture.Blazor.Application.Common.Models;
using CleanArchitecture.Blazor.Application.Features.Devices.Queries.Pagination;
using CleanArchitecture.Blazor.Application.Features.Devices.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.Devices.Commands.Delete;

namespace CleanArchitecture.Blazor.Application.Common.Interfaces.Services;

public interface IDeviceCrudService
{
    Task<PaginatedData<DeviceDto>> GetDevicesWithPaginationAsync(DevicesWithPaginationQuery query, CancellationToken cancellationToken = default);
    Task<Result<int>> SaveDeviceAsync(AddEditDeviceCommand command, CancellationToken cancellationToken = default);
    Task<Result> DeleteDevicesAsync(DeleteDeviceCommand command, CancellationToken cancellationToken = default);
}

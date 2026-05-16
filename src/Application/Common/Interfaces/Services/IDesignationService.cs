// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Designations.DTOs;

namespace CleanArchitecture.Blazor.Application.Common.Interfaces.Services;

public interface IDesignationService
{
    Task<List<DesignationDto>> GetAllDesignationsAsync(CancellationToken cancellationToken = default);
}
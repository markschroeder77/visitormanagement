// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.CheckinPoints.DTOs;

namespace CleanArchitecture.Blazor.Application.Common.Interfaces.Services;

public interface ICheckinPointService
{
    Task<List<CheckinPointDto>> GetAllCheckinPointsAsync(CancellationToken cancellationToken = default);
}
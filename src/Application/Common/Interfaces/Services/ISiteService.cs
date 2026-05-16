// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Sites.DTOs;

namespace CleanArchitecture.Blazor.Application.Common.Interfaces.Services;

public interface ISiteService
{
    Task<List<SiteDto>> GetAllSitesAsync(CancellationToken cancellationToken = default);
}
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Common.Models;
using CleanArchitecture.Blazor.Application.Features.KeyValues.DTOs;
using CleanArchitecture.Blazor.Application.Features.KeyValues.Queries.ByName;
using CleanArchitecture.Blazor.Application.Features.KeyValues.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.KeyValues.Commands.Delete;
using CleanArchitecture.Blazor.Application.Features.KeyValues.Queries.Export;
using CleanArchitecture.Blazor.Application.Features.KeyValues.Commands.Import;

namespace CleanArchitecture.Blazor.Application.Common.Interfaces.Services;

public interface IKeyValueService
{
    Task<List<KeyValueDto>> GetAllKeyValuesAsync(CancellationToken cancellationToken = default);
    Task<Result<int>> SaveKeyValueAsync(AddEditKeyValueCommand command, CancellationToken cancellationToken = default);
    Task<Result> DeleteKeyValuesAsync(DeleteKeyValueCommand command, CancellationToken cancellationToken = default);
    Task<byte[]> ExportKeyValuesAsync(ExportKeyValuesQuery query, CancellationToken cancellationToken = default);
    Task<Result> ImportKeyValuesAsync(ImportKeyValuesCommand command, CancellationToken cancellationToken = default);
}

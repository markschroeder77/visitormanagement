// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Common.Interfaces.Services;
using CleanArchitecture.Blazor.Application.Common.Models;
using CleanArchitecture.Blazor.Application.Features.KeyValues.DTOs;
using CleanArchitecture.Blazor.Application.Features.KeyValues.Queries.ByName;
using CleanArchitecture.Blazor.Application.Features.KeyValues.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.KeyValues.Commands.Delete;
using CleanArchitecture.Blazor.Application.Features.KeyValues.Queries.Export;
using CleanArchitecture.Blazor.Application.Features.KeyValues.Commands.Import;

namespace CleanArchitecture.Blazor.Application.Services.Features;

public class KeyValueService : IKeyValueService
{
    private readonly ISender _mediator;
    public KeyValueService(ISender mediator) => _mediator = mediator;

    public async Task<List<KeyValueDto>> GetAllKeyValuesAsync(CancellationToken ct = default)
        => (await _mediator.Send(new GetAllKeyValuesQuery(), ct)).ToList();

    public async Task<Result<int>> SaveKeyValueAsync(AddEditKeyValueCommand command, CancellationToken ct = default)
        => await _mediator.Send(command, ct);

    public async Task<Result> DeleteKeyValuesAsync(DeleteKeyValueCommand command, CancellationToken ct = default)
        => await _mediator.Send(command, ct);

    public async Task<byte[]> ExportKeyValuesAsync(ExportKeyValuesQuery query, CancellationToken ct = default)
        => await _mediator.Send(query, ct);

    public async Task<Result> ImportKeyValuesAsync(ImportKeyValuesCommand command, CancellationToken ct = default)
        => await _mediator.Send(command, ct);
}

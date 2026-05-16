// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Common.Interfaces.Services;
using CleanArchitecture.Blazor.Application.Common.Models;
using CleanArchitecture.Blazor.Application.Features.Products.DTOs;
using CleanArchitecture.Blazor.Application.Features.Products.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.Products.Commands.Delete;
using CleanArchitecture.Blazor.Application.Features.Products.Queries.Pagination;
using CleanArchitecture.Blazor.Application.Features.Products.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.Products.Commands.Delete;
using CleanArchitecture.Blazor.Application.Features.Products.Queries.Export;
using CleanArchitecture.Blazor.Application.Features.Products.Commands.Import;

namespace CleanArchitecture.Blazor.Application.Services.Features;

public class ProductCrudService : IProductCrudService
{
    private readonly ISender _mediator;
    public ProductCrudService(ISender mediator) => _mediator = mediator;

    public async Task<PaginatedData<ProductDto>> GetProductsWithPaginationAsync(ProductsWithPaginationQuery query, CancellationToken ct = default)
        => await _mediator.Send(query, ct);

    public async Task<Result<int>> SaveProductAsync(AddEditProductCommand command, CancellationToken ct = default)
        => await _mediator.Send(command, ct);

    public async Task<Result> DeleteProductsAsync(DeleteProductCommand command, CancellationToken ct = default)
        => await _mediator.Send(command, ct);

    public async Task<byte[]> ExportProductsAsync(ExportProductsQuery query, CancellationToken ct = default)
        => await _mediator.Send(query, ct);

    public async Task<Result> ImportProductsAsync(ImportProductsCommand command, CancellationToken ct = default)
        => await _mediator.Send(command, ct);
}

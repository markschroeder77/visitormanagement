// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Products.DTOs;
using CleanArchitecture.Blazor.Application.Common.Models;
using CleanArchitecture.Blazor.Application.Features.Products.Queries.Pagination;
using CleanArchitecture.Blazor.Application.Features.Products.Queries.Export;
using CleanArchitecture.Blazor.Application.Features.Products.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.Products.Commands.Delete;
using CleanArchitecture.Blazor.Application.Features.Products.Commands.Import;

namespace CleanArchitecture.Blazor.Application.Common.Interfaces.Services;

public interface IProductCrudService
{
    Task<PaginatedData<ProductDto>> GetProductsWithPaginationAsync(ProductsWithPaginationQuery query, CancellationToken cancellationToken = default);
    Task<Result<int>> SaveProductAsync(AddEditProductCommand command, CancellationToken cancellationToken = default);
    Task<Result> DeleteProductsAsync(DeleteProductCommand command, CancellationToken cancellationToken = default);
    Task<byte[]> ExportProductsAsync(ExportProductsQuery query, CancellationToken cancellationToken = default);
    Task<Result> ImportProductsAsync(ImportProductsCommand command, CancellationToken cancellationToken = default);
}

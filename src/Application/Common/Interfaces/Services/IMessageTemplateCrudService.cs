// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.MessageTemplates.DTOs;
using CleanArchitecture.Blazor.Application.Common.Models;
using CleanArchitecture.Blazor.Application.Features.MessageTemplates.Queries.Pagination;
using CleanArchitecture.Blazor.Application.Features.MessageTemplates.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.MessageTemplates.Commands.Delete;

namespace CleanArchitecture.Blazor.Application.Common.Interfaces.Services;

public interface IMessageTemplateCrudService
{
    Task<PaginatedData<MessageTemplateDto>> GetMessageTemplatesWithPaginationAsync(MessageTemplatesWithPaginationQuery query, CancellationToken cancellationToken = default);
    Task<Result<int>> SaveMessageTemplateAsync(AddEditMessageTemplateCommand command, CancellationToken cancellationToken = default);
    Task<Result> DeleteMessageTemplatesAsync(DeleteMessageTemplateCommand command, CancellationToken cancellationToken = default);
}

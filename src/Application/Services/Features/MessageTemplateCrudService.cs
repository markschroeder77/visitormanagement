// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Common.Interfaces.Services;
using CleanArchitecture.Blazor.Application.Common.Models;
using CleanArchitecture.Blazor.Application.Features.MessageTemplates.DTOs;
using CleanArchitecture.Blazor.Application.Features.MessageTemplates.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.MessageTemplates.Commands.Delete;
using CleanArchitecture.Blazor.Application.Features.MessageTemplates.Queries.Pagination;
using CleanArchitecture.Blazor.Application.Features.MessageTemplates.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.MessageTemplates.Commands.Delete;

namespace CleanArchitecture.Blazor.Application.Services.Features;

public class MessageTemplateCrudService : IMessageTemplateCrudService
{
    private readonly ISender _mediator;
    public MessageTemplateCrudService(ISender mediator) => _mediator = mediator;

    public async Task<PaginatedData<MessageTemplateDto>> GetMessageTemplatesWithPaginationAsync(MessageTemplatesWithPaginationQuery query, CancellationToken ct = default)
        => await _mediator.Send(query, ct);

    public async Task<Result<int>> SaveMessageTemplateAsync(AddEditMessageTemplateCommand command, CancellationToken ct = default)
        => await _mediator.Send(command, ct);

    public async Task<Result> DeleteMessageTemplatesAsync(DeleteMessageTemplateCommand command, CancellationToken ct = default)
        => await _mediator.Send(command, ct);
}

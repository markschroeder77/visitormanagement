// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Common.Messaging;

/// <summary>
/// Pre-processor interface run before the main handler.
/// Replaces MediatR's IRequestPreProcessor&lt;TRequest&gt;.
/// </summary>
public interface IRequestPreProcessor<in TRequest>
{
    Task Process(TRequest request, CancellationToken cancellationToken);
}

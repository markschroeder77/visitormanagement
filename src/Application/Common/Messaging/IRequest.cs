// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Common.Messaging;

/// <summary>
/// Represents a request/command/query that expects a response.
/// Replaces MediatR's IRequest&lt;TResponse&gt;.
/// </summary>
public interface IRequest<TResponse> { }

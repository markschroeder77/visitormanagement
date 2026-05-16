// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Common.Messaging;

/// <summary>
/// Publishes notifications to all registered handlers.
/// Replaces MediatR's IPublisher.
/// </summary>
public interface IPublisher
{
    Task Publish(INotification notification, CancellationToken cancellationToken = default);
}

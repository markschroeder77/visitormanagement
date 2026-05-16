// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Common.Messaging;

/// <summary>
/// Handler interface for processing notifications/events.
/// Replaces MediatR's INotificationHandler&lt;TNotification&gt;.
/// </summary>
public interface INotificationHandler<in TNotification>
    where TNotification : INotification
{
    Task Handle(TNotification notification, CancellationToken cancellationToken);
}

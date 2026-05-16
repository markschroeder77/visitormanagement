// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Blazor.Application.Common.Messaging;

/// <summary>
/// Publishes notifications to all registered handlers using the DI container.
/// Replaces MediatR's default publisher.
/// </summary>
public class Publisher : IPublisher
{
    private readonly IServiceProvider _serviceProvider;

    public Publisher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Publish(INotification notification, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(INotificationHandler<>).MakeGenericType(notification.GetType());
        var handlers = _serviceProvider.GetServices(handlerType);
        var method = handlerType.GetMethod("Handle")!;
        var tasks = new List<Task>();
        foreach (var handler in handlers)
        {
            var result = method.Invoke(handler, new object[] { notification, cancellationToken })!;
            tasks.Add((Task)result);
        }
        await Task.WhenAll(tasks);
    }
}

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace CleanArchitecture.Blazor.Application.Common.Messaging;

/// <summary>
/// Dispatches requests to their registered handlers using the DI container.
/// Supports pipeline behaviors, pre-processors, and post-processors.
/// Replaces MediatR's default sender.
/// </summary>
public class Sender : ISender
{
    private readonly IServiceProvider _serviceProvider;

    public Sender(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var requestType = request.GetType();
        var responseType = typeof(TResponse);

        // Resolve the actual handler
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, responseType);
        var handler = _serviceProvider.GetRequiredService(handlerType);
        var handleMethod = handlerType.GetMethod("Handle")!;

        // Build the pipeline: resolve all behaviors, pre-processors, and the handler itself
        var behaviorType = typeof(IPipelineBehavior<,>).MakeGenericType(requestType, responseType);
        var behaviors = _serviceProvider.GetServices(behaviorType).Reverse().ToArray();

        var preProcessorType = typeof(IRequestPreProcessor<>).MakeGenericType(requestType);
        var preProcessors = _serviceProvider.GetServices(preProcessorType).ToArray();

        var preProcessorMethod = typeof(IRequestPreProcessor<>).MakeGenericType(requestType).GetMethod("Process")!;

        // Run pre-processors
        foreach (var preProcessor in preProcessors)
        {
            await (Task)preProcessorMethod.Invoke(preProcessor, new object[] { request, cancellationToken })!;
        }

        if (behaviors.Length > 0)
        {
            // Build pipeline chain from innermost to outermost
            RequestHandlerDelegate<TResponse> handlerDelegate = () =>
                (Task<TResponse>)handleMethod.Invoke(handler, new object[] { request, cancellationToken })!;

            for (int i = behaviors.Length - 1; i >= 0; i--)
            {
                var behavior = behaviors[i];
                var behaviorHandleMethod = behaviorType.GetMethod("Handle")!;
                var next = handlerDelegate;
                handlerDelegate = () =>
                {
                    var result = behaviorHandleMethod.Invoke(behavior, new object[] { request, next, cancellationToken })!;
                    return (Task<TResponse>)result;
                };
            }

            return await handlerDelegate();
        }
        else
        {
            // No behaviors - call handler directly
            var result = handleMethod.Invoke(handler, new object[] { request, cancellationToken })!;
            return await (Task<TResponse>)result;
        }
    }
}

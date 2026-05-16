// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Common.Behaviours;
using CleanArchitecture.Blazor.Application.Features.ApprovalHistories.EventHandlers;
using CleanArchitecture.Blazor.Application.Services.MessageService;
using CleanArchitecture.Blazor.Application.Services.Picklist;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitecture.Blazor.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Register all request handlers, notification handlers, and pipeline behaviors
        RegisterMessageHandlers(services, Assembly.GetExecutingAssembly());

        // Register sender and publisher
        services.AddScoped<ISender, Sender>();
        services.AddScoped<IPublisher, Publisher>();

        foreach (var assembly in new[] { Assembly.GetExecutingAssembly() }) // add all your assemblies here
        {
            foreach (var createdEvent in assembly
                .DefinedTypes
                .Where(dt => !dt.IsAbstract && dt.IsSubclassOf(typeof(ApprovalHistoryCreatedEventHandler)))
            )
            {
                services.AddTransient(typeof(INotificationHandler<>).MakeGenericType(createdEvent), typeof(ApprovalHistoryCreatedEventHandler));
            }
        }

        services.AddLazyCache();
        services.AddScoped<SMSMessageService>();
        services.AddScoped<MailMessageService>();
        services.AddScoped<IPicklistService, PicklistService>();

        // Register domain service wrappers
        services.AddApplicationServices();

        return services;
    }

    private static void RegisterMessageHandlers(IServiceCollection services, Assembly assembly)
    {
        foreach (var type in assembly.GetTypes().Where(t => !t.IsAbstract && !t.IsInterface))
        {
            foreach (var iface in type.GetInterfaces())
            {
                if (iface.IsGenericType)
                {
                    var genericDef = iface.GetGenericTypeDefinition();
                    if (genericDef == typeof(IRequestHandler<,>) ||
                        genericDef == typeof(INotificationHandler<>))
                    {
                        services.AddTransient(iface, type);
                    }
                }
            }
        }

        // Register pipeline behaviors, pre/post-processors manually to avoid constraint mismatch issues
        // (implementation types have constraints like where TRequest : IRequest<TResponse>
        //  that the interface itself doesn't declare)
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheInvalidationBehaviour<,>));
        services.AddTransient(typeof(IRequestPostProcessor<,>), typeof(DomainEventPublishingBehaviour<,>));
        services.AddTransient(typeof(IRequestPreProcessor<>), typeof(LoggingBehaviour<>));
    }
}

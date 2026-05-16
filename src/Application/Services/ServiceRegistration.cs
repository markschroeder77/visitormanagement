// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Common.Interfaces.Services;
using CleanArchitecture.Blazor.Application.Services.Features;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Blazor.Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IVisitorService, VisitorService>();
        services.AddScoped<IVisitorHistoryService, VisitorHistoryService>();
        services.AddScoped<ISiteConfigurationService, SiteConfigurationService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<ISiteService, SiteService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IDesignationService, DesignationService>();
        services.AddScoped<ICheckinPointService, CheckinPointService>();
        services.AddScoped<IDeviceService, DeviceService>();

        // CRUD services
        services.AddScoped<ISiteCrudService, SiteCrudService>();
        services.AddScoped<IEmployeeCrudService, EmployeeCrudService>();
        services.AddScoped<IDepartmentCrudService, DepartmentCrudService>();
        services.AddScoped<IDesignationCrudService, DesignationCrudService>();
        services.AddScoped<IDeviceCrudService, DeviceCrudService>();
        services.AddScoped<ICheckinPointCrudService, CheckinPointCrudService>();
        services.AddScoped<IProductCrudService, ProductCrudService>();
        services.AddScoped<ISiteConfigurationCrudService, SiteConfigurationCrudService>();
        services.AddScoped<IMessageTemplateCrudService, MessageTemplateCrudService>();

        // Specialized services
        services.AddScoped<IVisitorHistoryCrudService, VisitorHistoryCrudService>();
        services.AddScoped<ILogService, LogService>();
        services.AddScoped<IAuditTrailService, AuditTrailService>();
        services.AddScoped<IKeyValueService, KeyValueService>();
        return services;
    }
}

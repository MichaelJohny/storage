using Microsoft.Extensions.DependencyInjection;
using Storage.Application.Common.Interfaces;
using Storage.Infrastructure.Services;

namespace Storage.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<ICurrentUserService, CurrentUserService>();

        services.AddSingleton<IStorageService, GoogleStorageService>();
        return services;
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Storage.Application.Common.Interfaces;

namespace Storage.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StorageDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("StorageConnection"), ops =>
                {
                    ops.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
                    ops.EnableRetryOnFailure(maxRetryCount: 6, maxRetryDelay: TimeSpan.FromSeconds(3), null);
                })
                .EnableSensitiveDataLogging());

        services.AddScoped<IStorageDbContext>(provider => provider.GetService<StorageDbContext>());
            
        return services;
    }
}
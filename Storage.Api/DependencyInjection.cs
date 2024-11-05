using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Storage.Application;
using Storage.Application.Common.Behaviours;
using Storage.Application.Common.Events;
using Storage.Application.Features.Commands;
using Storage.Infrastructure;
using Storage.Persistence;

namespace Storage.Api;

public  static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services, WebApplicationBuilder builder)
    {

        services.AddApplication()
            .AddPersistence(builder.Configuration)
            .AddInfrastructure();
        
        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        services.AddMassTransitDependency(builder);
        
        services.AddHttpContextAccessor();

        services.AddControllers()
            .AddFluentValidation(fv =>
            fv.RegisterValidatorsFromAssemblyContaining<UploadFileCommandValidator>()
        );
        return services;
    } 
    
      static IServiceCollection AddMassTransitDependency(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddMassTransit(config =>
            {
                config.AddConsumer<SaveFileConsumer>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(builder.Configuration.GetValue<string>("RabbitMQ:host"), "/", h =>
                    {
                        h.Username(builder.Configuration.GetValue<string>("RabbitMQ:username"));
                        h.Password(builder.Configuration.GetValue<string>("RabbitMQ:password"));
                    });
                    cfg.UseConsumeFilter(typeof(EventLoggingFilter<>), ctx);
                    cfg.ReceiveEndpoint("save-file-queue", e => { e.ConfigureConsumer<SaveFileConsumer>(ctx); });
                   
                });
            });

            return services;
        }
}
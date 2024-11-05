using MassTransit;
using Microsoft.Extensions.Logging;

namespace Storage.Application.Common.Behaviours;


public class EventLoggingFilter<TConsumer> : IFilter<ConsumeContext<TConsumer>> where TConsumer : class
{
    private readonly ILogger _logger;

    public EventLoggingFilter(ILogger<EventLoggingFilter<TConsumer>> logger)
    {
        _logger = logger;
    }

    public async Task Send(ConsumeContext<TConsumer> context, IPipe<ConsumeContext<TConsumer>> next)
    {
        var eventId = Guid.NewGuid();
        using (_logger.BeginScope(new Dictionary<string, object>
                   {["name"] = typeof(TConsumer).Name, ["ConsumerEventId"] = eventId}))
        {
            _logger.LogInformation("---Pre Processing Consumer: '{@name}' received message.",
                typeof(TConsumer).Name);
            await next.Send(context);
            _logger.LogInformation("---Post Processing: Consumer '{@name}' processed message.",
                typeof(TConsumer).Name);
        }
    }

    public void Probe(ProbeContext context)
    {
        throw new NotImplementedException();
    }
}
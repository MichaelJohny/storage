using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Storage.Application.Common.Interfaces;

namespace Storage.Application.Common.Behaviours;

public class ResponseLogger<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
{
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;
    public ResponseLogger(ILogger<TResponse> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }
    public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
    {
        var name = typeof(TRequest).Name;

        _logger.LogInformation("---Post Storage Processing: {Name} {@UserId}",
            name, _currentUserService.UserId);
        return Task.CompletedTask;
    }
}
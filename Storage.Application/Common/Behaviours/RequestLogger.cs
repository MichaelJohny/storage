using MediatR.Pipeline;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Storage.Application.Common.Interfaces;

namespace Storage.Application.Common.Behaviours;

public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
{
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;
    public RequestLogger(ILogger<TRequest> logger, 
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var name = typeof(TRequest).Name;
        _logger.LogInformation("---Pre Storage Processing: {Name} {@UserId} {@Request} ",
            name, _currentUserService.UserId, request );
        return Task.CompletedTask;
    }
}
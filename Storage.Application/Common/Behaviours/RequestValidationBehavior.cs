using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Storage.Application.Common.Behaviours;

public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<RequestValidationBehavior<TRequest, TResponse>> _logger;

    public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators,
        ILogger<RequestValidationBehavior<TRequest, TResponse>> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var failures = _validators.Select(v => v.Validate(context)).SelectMany(result => result.Errors).Where(f => f != null).ToList();
        if (failures.Count != 0)
        {
            throw new ValidationException(failures);
        }
        return next();
    }
}
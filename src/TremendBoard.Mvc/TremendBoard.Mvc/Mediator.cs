using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace TremendBoard.Api.Setup;


public sealed class ValidatorPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidatorPipeline(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (_validators.Any())
        {
            ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);
            List<ValidationFailure> list = (await Task.WhenAll(_validators.Select((IValidator<TRequest> v) => v.ValidateAsync(context, cancellationToken)))).Where((ValidationResult r) => r.Errors.Any()).SelectMany((ValidationResult r) => r.Errors).ToList();
            if (list.Any())
            {
                throw new ValidationException(list);
            }
        }

        return await next();
    }
}

public static class Mediator
{
	public static IServiceCollection AddMediatorConfig(this IServiceCollection services, params Assembly[] assemblies)
	{
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorPipeline<,>));
		services.AddMediatR(assemblies);
        
		return services;
	}
}
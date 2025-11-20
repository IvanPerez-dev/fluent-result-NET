using FluentValidation;
using MediatR;
using AppContext = Osom.FluentRestult.Application.Exceptions;

namespace Osom.FluentRestult.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken
        )
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken))
                );

                var failures = validationResults
                    //.Select( v => v.ValidateAsync(context))
                    .SelectMany(result => result.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count != 0)
                {
                    var failuresGruped = failures.GroupBy(x => x.PropertyName);

                    var errors = failuresGruped.Select(x => new KeyValuePair<string, List<string>>(
                        x.Key,
                        x.Select(y => y.ErrorMessage).ToList()
                    ));

                    throw new AppContext.ValidationException(errors);
                }
            }
            return await next();
        }
    }
}

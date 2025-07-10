using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Reflection;

namespace Application.Behaviors;

internal class ValidationPipelineBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : IResult
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next();
        }

        List<ValidationError> validationErrors = new();

        foreach (IValidator<TRequest> validator in validators)
        {
            ValidationResult validationResult = await validator.ValidateAsync(request);
            
            if (!validationResult.IsValid)
            {
                validationErrors.AddRange(validationResult.AsErrors());
            }
        }

        if (validationErrors.Any())
        {
            return CreateValidationResult<TResponse>(validationErrors);
        }

        return await next();
    }

    private static TResult CreateValidationResult<TResult>(List<ValidationError> errors) where TResult : IResult
    {
        Type genericTypeDefinition = typeof(TResult).GetGenericTypeDefinition();
        Type genericType = genericTypeDefinition.MakeGenericType(typeof(TResult).GenericTypeArguments[0]);
        MethodInfo method = genericType.GetMethod(nameof(Result<object>.Invalid), new Type[] { typeof(ValidationError[]) }) ?? throw new Exception();

        object result = method.Invoke(null, new object?[] { errors.ToArray() }) ?? throw new Exception();

        return (TResult)result;
    }
}

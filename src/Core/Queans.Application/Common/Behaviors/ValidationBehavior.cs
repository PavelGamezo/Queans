﻿using ErrorOr;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Reflection;

namespace Queans.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> 
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
    {
        private readonly IValidator<TRequest>? _validator;

        public ValidationBehavior(IValidator<TRequest>? validator) 
            => _validator = validator;

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (_validator is null)
            {
                return await next();
            }

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid)
            {
                return await next();
            }

            return TryCreateResponseFromErrors(validationResult.Errors, out var response)
                ? response
                : throw new ValidationException(validationResult.Errors);
        }

        private static bool TryCreateResponseFromErrors(List<ValidationFailure> validationFailures, out TResponse response)
        {
            List<Error> errors = validationFailures.ConvertAll(failure => Error.Validation(
                code: failure.PropertyName,
                description: failure.ErrorMessage));

            response = (TResponse?)typeof(TResponse)
                .GetMethod(
                    name: nameof(ErrorOr<object>.From),
                    bindingAttr: BindingFlags.Static | BindingFlags.Public,
                    types: new[] { typeof(List<Error>) })?
                .Invoke(null, new[] { errors })!;

            return response is not null;
        }
    }
}

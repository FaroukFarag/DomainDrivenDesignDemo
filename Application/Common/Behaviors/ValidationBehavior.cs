﻿using Application.Authentication.Commands.Register;
using Application.Authentication.Common;
using ErrorOr;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
    {
        private readonly IValidator<TRequest>? _validator;

        public ValidationBehavior(IValidator<TRequest>? validator = null)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if(_validator is null)
                return await next();

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if(validationResult.IsValid)
                return await next();

            var errors = validationResult.Errors.ConvertAll(error => Error.Validation(error.PropertyName, error.ErrorMessage));

            return (dynamic)errors;
        }
    }
}

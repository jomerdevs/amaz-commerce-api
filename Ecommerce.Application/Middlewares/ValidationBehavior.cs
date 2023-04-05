using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Middlewares
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        // evaluar las futuras validaciones que se disparen cuando se trate por ejemplo de insertar a futuro un producto o cualquier registro
        public async Task<TResponse> Handle(
            TRequest request, 
            RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken
            )
        {
            if ( _validators.Any() )
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators
                    .Select(v => v.ValidateAsync(context, cancellationToken)));

                // capturamos los errores
                var failures = validationResults.SelectMany(r => r.Errors)
                    .Where( f => f != null)
                    .ToList();

                // si hay errores lanzamos la excepción
                if (failures.Count != 0)
                {
                    throw new ValidationException(failures);
                }
            }

            // si no hay mensajes de validación le indicamos que siga el programa
            return await next();
        }
    }
}

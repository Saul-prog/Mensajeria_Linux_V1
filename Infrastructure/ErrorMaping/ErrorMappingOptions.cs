using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Mensajeria_Linux.Infrastructure.Domain;
using Mensajeria_Linux.Infrastructure.Responses;
using Mensajeria_Linux.Infrastructure.ErrorMapping;

namespace Mensajeria_Linux.Infrastructure.ErrorMapping
{
    /// <summary>
    /// Administra las opciones y la configuración para el mapeo de los errores de la aplicación. A diferencia de la primera, proporciona funciones de mapeo de errores y ofrece métodos para un mapeo de un error a una respuesta de error más concreta.
    /// </summary>
    public class ErrorMappingOptions
    {
        private readonly Dictionary<Type, Func<HttpContext, BaseError, IErrorResponse>> mappers
            = new Dictionary<Type, Func<HttpContext, BaseError, IErrorResponse>>();

        private Action<HttpContext, BaseError, IErrorResponse> actionAfterMap = null;

        public ErrorMappingOptions AddMap<T>(Func<HttpContext, T, IErrorResponse> map)
            where T : BaseError
        {
            Func<HttpContext, BaseError, IErrorResponse> mapTo =
                (httpContext, error) => map(httpContext, (T)error);

            mappers.Add(typeof(T), mapTo);
            return this;
        }

        /// <summary>
        /// Action to be executed after an error is mapped
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public ErrorMappingOptions AfterMap(Action<HttpContext, BaseError, IErrorResponse> action)
        {
            actionAfterMap = action;
            return this;
        }

        public IErrorResponse Map(HttpContext ctx, BaseError data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data), "Unable to map null value");

            var errorType = data.GetType();
            if (!mappers.ContainsKey(errorType))
                throw new ArgumentException($"Unable to find mapping for type: {errorType.FullName}. Please register it through '{nameof(AddMap)}' method ");
            var map = mappers[errorType];
            try
            {
                var mappedData = map(ctx, data);
                actionAfterMap?.Invoke(ctx, data, mappedData);
                return mappedData;
            }
            catch (Exception ex)
            {
                throw new ErrorMappingException($"Error while mapping type '{errorType.FullName}'. See inner exception to view details", ex);
            }
        }
    }
}

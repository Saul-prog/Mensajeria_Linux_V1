using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Mensajeria_Linux.Infrastructure.Responses;
using Mensajeria_Linux.Infrastructure.Domain;

namespace Mensajeria_Linux.Infrastructure.ErrorMapping
{
    /// <summary>
    /// Es una implementación de IErrorMapper que mapea los diferentes errores para convertirlos en respuestas de error de aplicación.
    /// </summary>
    public class DefaultErrorMapper : IErrorMapper
    {
        private readonly IOptions<ErrorMappingOptions> options;

        public DefaultErrorMapper(IOptions<ErrorMappingOptions> options)
        {
            this.options = options;
        }

        public IErrorResponse GetError(HttpContext ctx, BaseError input)
        {
            return options.Value.Map(ctx, input);
        }
    }
}

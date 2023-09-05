using Microsoft.AspNetCore.Http;
using Mensajeria_Linux.Infrastructure.Responses;
using Mensajeria_Linux.Infrastructure.Domain;

namespace Mensajeria_Linux.Infrastructure.ErrorMapping
{
    /// <summary>
    /// Es la interfaz de ErrorMapper
    /// </summary>
    public interface IErrorMapper
    {
        IErrorResponse GetError(HttpContext ctx, BaseError input);
    }
}

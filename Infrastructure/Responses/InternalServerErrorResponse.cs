using System.Net;

namespace Mensajeria_Linux.Infrastructure.Responses
{
    /// <summary>
    /// La respuesta de un error interno
    /// </summary>
    public class InternalServerErrorResponse : BaseErrorResponse
    {
        public InternalServerErrorResponse(
            string title,
            string detail,
            string instance)
            : base(title, detail, instance, HttpStatusCode.InternalServerError, "internal-server-error")
        {
        }
    }
}

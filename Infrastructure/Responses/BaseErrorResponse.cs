using System.Net;

namespace Mensajeria_Linux.Infrastructure.Responses
{
    /// <summary>
    /// es la respuesta que devuelve InternalServerErrorResponse
    /// </summary>
    public class BaseErrorResponse : IErrorResponse
    {

        protected BaseErrorResponse(string title, string detail, string instance, HttpStatusCode status, string type)
        {
            Title = title;
            Status = status;
            Instance = instance;
            Detail = detail;
            Type = type;
        }

        public string Title { get; }

        public string Detail { get; }

        public HttpStatusCode Status { get; }

        public string Instance { get; }

        public string Type { get; set; }
    }
}

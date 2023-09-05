using System;

namespace Mensajeria_Linux.Infrastructure.ErrorMapping
{
    /// <summary>
    /// Hereda de la clase Error, se utiliza para manejar los errores generados por los mapeos.
    /// </summary>
    [Serializable]
    public class ErrorMappingException : Exception
    {
        public ErrorMappingException()
        {
        }

        public ErrorMappingException(string message)
            : base(message)
        {
        }

        public ErrorMappingException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ErrorMappingException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}

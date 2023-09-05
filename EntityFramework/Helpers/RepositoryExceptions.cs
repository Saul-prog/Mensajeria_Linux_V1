using System.Globalization;

namespace Mensajeria_Linux.EntityFramework.Helpers
{
    /// <summary>
    /// Gestor de excepciones de la base de datos
    /// </summary>
    public class RepositoryExceptions : Exception
    {
        public RepositoryExceptions() : base() { }

        public RepositoryExceptions(string message) : base(message) { }

        public RepositoryExceptions(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args)) { }
    }
}

using Mensajeria_Linux.Business.Models;

namespace Mensajeria_Linux.Controllers.Teams.Models
{
    /// <summary>
    /// Clase para enviar un mensaje en teams
    /// </summary>
    public class EnviarTeams
    {
        /// <summary>
        /// Datos que se va a utilizar para rellenar la plantilla
        /// </summary>
        public AutorizacionDatos datos { get; set; }
        /// <summary>
        /// Plantilla que se va a utilizar
        /// </summary>
        public  string plantilla { get; set; }
        /// <summary>
        /// Nombre del webhook que se va a usar
        /// </summary>
        public string nombre { get; set; }
        /// <summary>
        /// Nombre de la agencia que va a mandar el mensaje
        /// </summary>
        public string nombreAgencia { get; set; }
        /// <summary>
        /// Token de la agencia que va a mandar el mensaje
        /// </summary>
        public string? tokenAgencia { get; set; }
        /// <summary>
        /// Email de administrador de agencia
        /// </summary>
        public string? adminEmail { get; set; }
        /// <summary>
        /// Token de adminsitrador de agencia
        /// </summary>
        public string? adminToken { get; set; }
    }
}

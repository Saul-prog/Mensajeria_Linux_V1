using Mensajeria_Linux.Business.Models;

namespace Mensajeria_Linux.Controllers.Email.Models
{
    /// <summary>
    /// Clase para enviar un email
    /// </summary>
    public class EnviarEmail
    {
        /// <summary>
        /// Nombre de la agencia que va a enviar el email
        /// </summary>
        public string nombreAgencia { get; set; } 
        /// <summary>
        /// Token de la agencia que va a enviar el email
        /// </summary>
        public string? tokenAgencia { get; set; }
        /// <summary>
        /// Lista de los destinatarios, con email y nombre
        /// </summary>
        public List<DatosEmail> emailsDestino { get; set; }
        /// <summary>
        /// Plantilla que se va a usar
        /// </summary>
        public string plantilla { get; set; }
        /// <summary>
        /// Datos para rellenar la plantilla AutorizacionDatos
        /// </summary>
        public AutorizacionDatos? autorizacionDatos { get; set; }
        /// <summary>
        /// Email de origen
        /// </summary>
        public string emailOrigen { get; set; }
        /// <summary>
        /// Asunto del Email
        /// </summary>
        public string asunto { get; set; }
        /// <summary>
        /// Ficheros que se van a enviar con nombre, fichero codificado en base64 y extensión del fichero
        /// </summary>
        public  List<DatosFichero>? ficheros { get; set; }
        /// <summary>
        /// Email para la autenticación
        /// </summary>
        public string? emailAdmin { get; set; }
        /// <summary>
        /// Token para la autenticación
        /// </summary>
        public string? tokenAdmin { get; set; }
    }
}

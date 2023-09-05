using Mensajeria_Linux.EntityFramework.Models.InfoSMS;

namespace Mensajeria_Linux.Controllers.SMSC.Models
{
    /// <summary>
    /// Clase que contiene los datos para actualizar un SMS
    /// </summary>
    public class ActualizarSMS
    {
        /// <summary>
        /// Datos de una petición de actualización de un SMS
        /// </summary>
        public UpdateInfoSMSRequest SMSRequest { get; set; }
        /// <summary>
        /// email de autentificación de administrador
        /// </summary>
        public string adminEmail { get; set; }
        /// <summary>
        /// token de autentificación de administrador
        /// </summary>
        public string adminToken { get; set; }
    }
}

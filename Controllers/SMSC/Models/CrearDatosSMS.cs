using Mensajeria_Linux.EntityFramework.Models.InfoSMS;

namespace Mensajeria_Linux.Controllers.SMSC.Models
{
    /// <summary>
    /// Clase para crear datos sms
    /// </summary>
    public class CrearDatosSMS
    {
        /// <summary>
        /// Datos necesarios para crear un sms
        /// </summary>
        public CreateInfoSMSRequest sMSRequest { get; set; }
        /// <summary>
        /// email de autentificación de administrador
        /// </summary>
        public string? adminEmail { get; set; }
        /// <summary>
        /// token de autentificación de administrador
        /// </summary>
        public string? adminToken { get; set; }
    }
}

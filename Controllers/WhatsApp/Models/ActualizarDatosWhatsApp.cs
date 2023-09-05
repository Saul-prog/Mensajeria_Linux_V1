using Mensajeria_Linux.EntityFramework.Models.InfoWhatsApp;

namespace Mensajeria_Linux.Controllers.WhatsApp.Models
{
    /// <summary>
    /// Clase para actualizar los datos de WhatsApp
    /// </summary>
    public class ActualizarDatosWhatsApp
    {
        /// <summary>
        /// Petición para actualizar los datos de WhatsApp
        /// </summary>
        public UpdateInfoWhatsAppRequest datosActualizar { get; set; }
        /// <summary>
        /// Nombre de la agencia
        /// </summary>
        public string agenciaNombre { get; set; }
        /// <summary>
        /// Token de la agencia
        /// </summary>
        public string? agenciaToken { get; set; }
        /// <summary>
        /// Email de administrador de la agencia
        /// </summary>
        public string? adminEmail { get; set; }
        /// <summary>
        /// Token de administrador de la agencia
        /// </summary>
        public string? adminToken { get; set; }

    }
}

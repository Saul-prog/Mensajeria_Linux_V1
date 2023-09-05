using Mensajeria_Linux.EntityFramework.Models.InfoWhatsApp;
using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Linux.Controllers.WhatsApp.Models
{
    /// <summary>
    /// Clase para crear los datos de WhatsApp
    /// </summary>
    public class CrearDatosWhatsApp
    {
        /// <summary>
        /// Petición para crear los datos de WhatsApp
        /// </summary>
        [Required]
        public CreateInfoWhatsAppRequest datosWhatsApp { get; set; }
        /// <summary>
        /// Nombre de la agencia
        /// </summary>
        [Required]
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

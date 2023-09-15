using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Linux.Controllers.AgenciaC.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ActualizarAgencia
    {
        /// <summary>
        /// Nombre de la agencia a actualizar
        /// </summary>
        public string? nombreAgencia { get; set; }

        /// <summary>
        /// Token de la agencia a actualizar
        /// </summary>
        public string? tokenAgencia { get; set; }
        /// <summary>
        /// Si puede o no enviar Email
        /// </summary>
        public bool puedeEmail { get; set; }
        /// <summary>
        /// Si puede o no enviar Teams
        /// </summary>
        public bool puedeTeams { get; set; }
        /// <summary>
        /// Si puede o no enviar SMS
        /// </summary>
        public bool puedeSMS { get; set; }
        /// <summary>
        /// Si puede o no enviar WhatsApp
        /// </summary>
        public bool puedeWhatsApp { get; set; }
        /// <summary>
        /// Email del administrador
        /// </summary>
        [Required]
        public string emailAdmin { get; set; }
        /// <summary>
        /// Token del administrador
        /// </summary>
        [Required]
        public string tokenAdmin { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mensajeria_Linux.Controllers.AgenciaC.Models
{
    public class CrearAgencia
    {
        /// <summary>
        /// Nombre de la agencia a crear
        /// </summary>
        [Required]
        public string nombreAgencia { get; set; }
        /// <summary>
        /// Si puede o no enviar Email
        /// </summary>
        [Required]
        public bool puedeEmail { get; set; }
        /// <summary>
        /// Si puede o no enviar Teams
        /// </summary>
        [Required]
        public bool puedeTeams { get; set; }
        /// <summary>
        /// Si puede o no enviar SMS
        /// </summary>
        [Required]
        public bool puedeSMS { get; set; }
        /// <summary>
        /// Si puede o no enviar WhatsApp
        /// </summary>
        [Required]
        public bool puedeWhatsApp { get; set; }
        /// <summary>
        /// Email del administrador
        /// </summary>
        public string emailAdmin { get; set; }
        /// <summary>
        /// Token del administrador
        /// </summary>
        public string tokenAdmin { get; set; }
    }
}

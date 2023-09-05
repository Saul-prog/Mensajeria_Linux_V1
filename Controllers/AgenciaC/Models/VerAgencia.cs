

using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Linux.Controllers.AgenciaC.Models
{
    public class VerAgencia
    {
        /// <summary>
        /// Nombre de la agencia a eliminar
        /// </summary>
        [Required]
        public string nombreAgencia { get; set; }
        /// <summary>
        /// Email del administrador
        /// </summary>
        public string? emailAdmin { get; set; }
        /// <summary>
        /// Token del administrador
        /// </summary>
        public string? tokenAdmin { get; set; }
    }
}

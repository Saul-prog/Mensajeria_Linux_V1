using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Linux.Controllers.PlantillasC.Models
{
    /// <summary>
    /// Clase para actualizar una plantilla
    /// </summary>
    public class ActualizarPlantilla
    {
        /// <summary>
        /// Nombre que se va a usar para identificar la plantilla
        /// </summary>
        [Required]
        public string nombre { get; set; }
        /// <summary>
        /// Plantilla en HTML
        /// </summary>
        public string? plantillaHtml { get; set; }
        /// <summary>
        /// Fichero para plantillas en JSON
        /// </summary>
        [Required]
        public IFormFile file { get; set; }
        /// <summary>
        /// Plantilla en texto plano
        /// </summary> 
        public string? plantillaPlana { get; set; }
        /// <summary>
        /// Correo de autentificacion de administrador
        /// </summary>
        [Required]
        public string correoAmin { get; set; }
        /// <summary>
        /// Nombre de la agencia a la que pertenece la plantilla
        /// </summary>
        [Required]
        public string agenciaNombre { get; set; }
        /// <summary>
        /// Token identificatorio de la agencia
        /// </summary>
        public string? tokenAgencia { get; set; }
        /// <summary>
        /// Token identificatorio de administrador
        /// </summary>
        public string? tokenAdmin { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Linux.Controllers.PlantillasC.Models
{
    /// <summary>
    /// Clase para crear plantillas
    /// </summary>
    public class CrearPlantilla
    {
        /// <summary>
        /// Nombre de la plantilla
        /// </summary>
        [Required]
        public string nombre { get; set; }
        /// <summary>
        /// Plantilla en HTML
        /// </summary>
        [Required]
        public string plantillaHtml { get; set; }
        /// <summary>
        /// Plantilla en texto plano
        /// </summary>
        [Required]
        public string plantillaPlana { get; set; }
        /// <summary>
        /// Plantilla en un fichero en JSON
        /// </summary>
        public IFormFile file { get; set; }
        /// <summary>
        /// Correo de identificación de adminsitrador
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
        public string? tokenAgencia { get;set; }
        /// <summary>
        /// Token identificatorio de administrador
        /// </summary>
        public string? tokenAdmin { get; set; }
    }
}

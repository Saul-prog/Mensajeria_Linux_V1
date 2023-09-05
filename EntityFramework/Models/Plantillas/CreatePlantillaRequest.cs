using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Linux.EntityFramework.Models.Plantillas
{
    /// <summary>
    /// Petición para crear una plantilla
    /// </summary>
    public class CreatePlantillaRequest
    {
        [Required]
        public string nombre { get; set; }       
        [Required]
        public string plantillaHtml { get; set; }
        [Required]
        public byte[] plantillaJSON { get; set; }
        [Required]
        public string plantillaPlana { get; set; }        
    }
}

using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Linux.EntityFramework.Models.Plantillas
{
    /// <summary>
    /// Petición para actualizar una plantilla
    /// </summary>
    public class UpdatePlantillaRequest
    {
        [Required]
        public string nombre { get; set; }
        
        public string plantillaHtml { get; set; }
        
        public byte[] plantillaJSON { get; set; }
        
        public string plantillaPlana { get; set; }
        public DateTime Updated { get; set; } = DateTime.Now;
    }
}

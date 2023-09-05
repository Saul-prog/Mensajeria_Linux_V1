using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Linux.EntityFramework.Models.InfoWhatsApp
{
    /// <summary>
    /// Petición para actualizar un WhatsApp
    /// </summary>
    public class UpdateInfoWhatsAppRequest
    {
        [Required]
        public string token { get; set; }
        public string idtelefono { get; set; }
        public string telefono { get; set; }
        public string plantilla { get; set; }
        public string idioma { get; set; }
        public string nombre { get; set; }
        public DateTime Updated { get; set; } = DateTime.Now;
        public int usuarioId { get; set; }
    }
}

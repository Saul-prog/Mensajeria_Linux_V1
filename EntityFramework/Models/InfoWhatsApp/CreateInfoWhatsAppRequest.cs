using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Linux.EntityFramework.Models.InfoWhatsApp
{
    /// <summary>
    /// Petición para crear un WhatsApp
    /// </summary>
    public class CreateInfoWhatsAppRequest
    {
        [Required]
        public string token { get; set; }
        public string idioma { get; set; }
        public string nombre { get; set; }
        public string nombreAgencia { get; set; }
        public string tokenAgencia { get; set; }
        public DateTime Created { get; set; }
    }
}

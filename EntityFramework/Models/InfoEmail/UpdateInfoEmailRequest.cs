using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Linux.EntityFramework.Models.InfoEmail
{
    /// <summary>
    /// Petición para actualizar un Email
    /// </summary>
    public class UpdateInfoEmailRequest
    {
        [Required]
        public string nombreAgencia { get; set; }
        [Required]
        public string tokenAgencia { get; set; }
        
        public string host { get; set; }
        
        public int port { get; set; }
        
        public string emailOrigen { get; set; }
        
        public string tokenEmail { get; set; }
        
        public string emailNombre { get; set; }
        public DateTime Updated { get; set; } = DateTime.Now;
    }
}

using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Linux.EntityFramework.Models.InfoEmail
{
    /// <summary>
    /// Petición para crear un email
    /// </summary>
    public class CreateInfoEmailRequest
    {
        [Required]
        public string nombreAgencia { get; set; }
        [Required]
        public string tokenAgencia { get; set; }
        [Required]
        public string host { get; set; }
        [Required]
        public int port { get; set; }
        [Required]
        public string emailOrigen { get; set; }
        [Required]
        public string emailTokenPassword { get; set; }
        [Required]
        public string emailNombre { get; set; }
        public DateTime Created { get; set; }
    }
}

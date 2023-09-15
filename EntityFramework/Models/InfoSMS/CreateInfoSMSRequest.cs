using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Linux.EntityFramework.Models.InfoSMS
{
    /// <summary>
    /// Petición para crear un SMS
    /// </summary>
    public class CreateInfoSMSRequest
    {
        [Required]
        public string nombre { get; set; }
        [Required]
        public string awsAcceskey { get; set; }
        [Required]
        public string awsSecretKey { get; set; }
        [Required]
        public string nombreAgencia { get; set; }
        public string? tokenAgencia { get; set; }
        public DateTime Created { get; set; }

    }
}

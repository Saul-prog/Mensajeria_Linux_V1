using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Linux.EntityFramework.Models.InfoSMS
{
    /// <summary>
    /// Petición para actualizar un sms
    /// </summary>
    public class UpdateInfoSMSRequest
    {
        public string nombre { get; set; }     
        public string awsAcceskey { get; set; }      
        public string awsSecretKey { get; set; }      
        public string nombreAgencia { get; set; }

        public string tokenAgencia { get; set; }
        public DateTime Updated { get; set; } = DateTime.Now;
    }
}

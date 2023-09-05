namespace Mensajeria_Linux.Controllers.SMSC.Models
{
    /// <summary>
    /// Clase para enviar un sms
    /// </summary>
    public class EnviarSMS
    {
        /// <summary>
        /// Números a los que se les va a enviar el SMS
        /// </summary>
        public List<string> numero { get; set; }
        /// <summary>
        /// Mensaje que se va a enviar
        /// </summary>
        public string mensaje { get; set; }
        /// <summary>
        /// Nombre de la agencia que lo envía
        /// </summary>
        public string agenciaNombre { get; set; }
        /// <summary>
        /// token de la agencia que lo envía
        /// </summary>
        public string? agenciaToken { get; set; }
        /// <summary>
        /// email de administrador de la agencia
        /// </summary>
        public string? adminEmail { get; set; }
        /// <summary>
        /// plantilla que se va a usar para el sms
        /// </summary>
        public string plantilla { get; set; }
        /// <summary>
        /// token de administrador de la agencia
        /// </summary>
        public string? adminToken { get; set; }
    }
}

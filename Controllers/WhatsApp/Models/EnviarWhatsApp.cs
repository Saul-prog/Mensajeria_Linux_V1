namespace Mensajeria_Linux.Controllers.WhatsApp.Models
{
    /// <summary>
    /// Clase para enviar datos de WhatsApp
    /// </summary>
    public class EnviarWhatsApp
    {
        /// <summary>
        /// Números a los que se le va a enviar el WhatsApp
        /// </summary>
        public List<string> numeros { get; set; }
        /// <summary>
        /// Nombre de los datos de WhatsApp
        /// </summary>
        public string nombre { get; set; }
        /// <summary>
        /// Plantilla que va a usar WhatsApp al enviar los datos
        /// </summary>
        public string template { get; set; }
        /// <summary>
        /// Nombre de la agencia
        /// </summary>
        public string agenciaNombre { get; set; }
        /// <summary>
        /// Plantilla para crear el mensaje de WhatsApp
        /// </summary>
        public string plantilla { get; set; }
        /// <summary>
        /// Datos para rellenar el WhatsApp
        /// </summary>
        public string mensaje { get; set; }
        /// <summary>
        /// Token de la agencia
        /// </summary>
        public string agenciaToken { get; set; }
        /// <summary>
        /// Email de administrador de agencia
        /// </summary>
        public string adminEmail { get; set; }
        /// <summary>
        /// Token de administrador de agencia
        /// </summary>
        public string adminToken { get; set; }
    }
}

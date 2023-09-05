namespace Mensajeria_Linux.EntityFramework.Entities
{
    /// <summary>
    /// Entidad de InfoWhatsApp
    /// </summary>
    public class InfoWhatsApp : Base
    {
        /// <summary>
        /// Token de envío de WhatsApp
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// Idioma que va a ser enviado el WhatsApp
        /// </summary>
        public string idioma { get; set; }
        /// <summary>
        /// Nombre del registro de WhatsApp
        /// </summary>
        public string nombre { get; set; }
        /// <summary>
        /// Identificador de la agencia a la que pertenece
        /// </summary>
        public int agenciaId { get; set; }
        /// <summary>
        /// Entidad de la agencia a la que pertenece
        /// </summary>
        public Agencia agencia { get; set; }
    }
}

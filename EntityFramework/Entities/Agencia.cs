namespace Mensajeria_Linux.EntityFramework.Entities
{
    /// <summary>
    /// Entidad de una agencia
    /// </summary>
    public class Agencia : Base
    {
        /// <summary>
        /// Nombre de la agencia
        /// </summary>
        public string nombreAgencia { get; set; }
        /// <summary>
        /// Identificador del administrador de creación
        /// </summary>
        public int AdminsitradorId { get; set; }
        /// <summary>
        /// Token de la agencia
        /// </summary>
        public string token {get;set;}
        /// <summary>
        /// Si la agencia puede usar Email
        /// </summary>
        public bool puedeEmail {get; set;}
        /// <summary>
        /// Si la agencia puede usar Teams
        /// </summary>
        public bool puedeTeams { get; set;}
        /// <summary>
        /// Si la agencia puede usar SMS
        /// </summary>
        public bool puedeSMS { get; set;}
        /// <summary>
        /// Si la agencia puede usar WhatsApp
        /// </summary>
        public bool puedeWhatsApp { get; set;}
        /// <summary>
        /// Administrador relacionado con  la agencia
        /// </summary>
        public Administradores adminsitrador { get; set; }
        /// <summary>
        /// Lista de InfoTeams relacionados con la agencia
        /// </summary>
        public IEnumerable<InfoTeams>? InfoTeams { get; set; }
        /// <summary>
        /// Lista de InfoWhatsApp relacionados con la agencia
        /// </summary>
        public IEnumerable<InfoWhatsApp>? InfoWhatsApps { get; set; }
        /// <summary>
        /// Lista de InfoEmail relacionados con la agencia
        /// </summary>
        public IEnumerable<InfoEmail>? InfoEmail { get; set; }
        /// <summary>
        /// Lista de InfoSMS relacionados con la agencia
        /// </summary>
        public IEnumerable<InfoSMS>? InfoSMS { get; set; }
        /// <summary>
        /// Lista de Plantillas relacionados con la agencia
        /// </summary>
        public IEnumerable<Plantillas>? Plantillas { get; set; }
    }
}
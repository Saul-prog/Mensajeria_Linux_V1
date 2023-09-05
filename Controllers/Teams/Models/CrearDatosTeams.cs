using Mensajeria_Linux.EntityFramework.Models.InfoTeam;

namespace Mensajeria_Linux.Controllers.Teams.Models
{
    /// <summary>
    /// Clase utilizada para crear datos de teams
    /// </summary>
    public class CrearDatosTeams
    {
        /// <summary>
        /// Petición para crear datos en teams
        /// </summary>
        public CreateInfoTeamsRequest model { get; set; }
        /// <summary>
        /// Email de administrador de agencia
        /// </summary>
        public string? adminEmail { get; set; }
        /// <summary>
        /// Token de administrador de agencia
        /// </summary>
        public string? adminToken { get; set; }
    }
}

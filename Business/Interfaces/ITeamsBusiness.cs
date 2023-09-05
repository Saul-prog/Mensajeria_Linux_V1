using Mensajeria_Linux.Controllers.Teams.Models;
using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.EntityFramework.Models.InfoTeam;

namespace Mensajeria_Linux.Business.Interfaces
{
    /// <summary>
    /// Interfaz para la capa business de Teams
    /// </summary>
    public interface ITeamsBusiness
    {
       /// <summary>
       /// Interfaz para devolver todos los datos de teams de una agencia
       /// </summary>
       /// <param name="agenciaNombre"></param>
       /// <param name="agenciaToken"></param>
       /// <param name="adminEmail"></param>
       /// <param name="adminToken"></param>
       /// <returns>Lista de datos de Teams</returns>
        Task<IEnumerable<InfoTeams>> GetAllInfoTeams (string agenciaNombre, string agenciaToken, string adminEmail, string adminToken);
        /// <summary>
        /// Interfaz que de devuelve un registro de teams de una agencia
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <returns>Datos de un Teams</returns>
        Task<InfoTeams> GetInfoTeamsByNombre (string nombre, string agenciaNombre, string agenciaToken, string adminEmail, string adminToken);
        /// <summary>
        /// Interfaz para crear datos de Teams
        /// </summary>
        /// <param name="model"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> CreateInfoTeams (CreateInfoTeamsRequest model, string adminEmail, string adminToken);
        /// <summary>
        /// Interfaz para actualizar un registro de Teams de un agencia
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> UpdateInfoTeams (int id, UpdateInfoTeamsRequest model, string adminEmail, string adminToken);
        /// <summary>
        /// Interfaz para eliminar un registro de Teams de una agencia
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> DeleteInfoTeams (int id, string agenciaNombre, string agenciaToken, string adminEmail, string adminToken);
        /// <summary>
        /// Interfaz para enviar un mensaje de Teams
        /// </summary>
        /// <param name="teamsBasico"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>

        Task<int> EnviarTeamsProcesoAutorizacion (EnviarTeams teamsBasico);

    }
}

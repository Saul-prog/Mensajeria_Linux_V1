using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.EntityFramework.Models.InfoTeam;

namespace Mensajeria_Linux.Services.Interfaces
{
    /// <summary>
    /// Interfaz de capa servicio de Team
    /// </summary>
    public interface IInfoTeamService
    {
        /// <summary>
        /// Interfaz para la creación de un registro Teams
        /// </summary>
        /// <param name="model"></param>
        /// <param name="agenciaId"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> CreateInfoTeams (CreateInfoTeamsRequest model, int agenciaId);

        /// <summary>
        /// Interfaz para la eliminación de un registro Teams
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agenciaId"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> DeleteInfoTeams (int id, int agenciaId);
        /// <summary>
        /// Interfaz para la actualización de un registro Teams
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="agenciaId"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> UpdateInfoTeams (int id, UpdateInfoTeamsRequest model, int agenciaId);
        /// <summary>
        /// Interfaz que devuelve todos los registros de teams de una agencia
        /// </summary>
        /// <param name="agenciaId"></param>
        /// <returns>Lista de InfoTeams</returns>
        Task<IEnumerable<InfoTeams>> GetAllInfoTeamsByAgenciaId (int agenciaId);
       /// <summary>
       /// Interfaz para obtener un registro de teams por nombre y agencia
       /// </summary>
       /// <param name="nombre"></param>
       /// <param name="agenciaId"></param>
       /// <returns>Un registro de Teams</returns>

        Task<InfoTeams> GetInfoTeamsByNombreAdnAgenciaId (string nombre, int agenciaId);
    }
}

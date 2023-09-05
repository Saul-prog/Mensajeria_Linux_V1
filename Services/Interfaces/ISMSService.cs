using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.EntityFramework.Models.InfoSMS;

namespace Mensajeria_Linux.Services.Interfaces
{
    /// <summary>
    /// Interfaz de  la capa servicio de SMS
    /// </summary>
    public interface ISMSService
    {
        /// <summary>
        /// Interfaz para la creación de un SMS
        /// </summary>
        /// <param name="model"></param>
        /// <param name="agenciaId"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> CreateInfoSMS (CreateInfoSMSRequest model, int agenciaId);
        /// <summary>
        /// Interfaz para la eliminación de un SMS
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agenciaId"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> DeleteSMS (int id, int agenciaId);
        /// <summary>
        /// Interfaz para la obtención de un registro SMS por id y agencia
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agenciaId"></param>
        /// <returns>Registro de InfoSMS</returns>
        Task<InfoSMS> GetInfoSMSlById (int id, int agenciaId);
        /// <summary>
        ///  Interfaz para la obtención de un registro SMS por nombre y agencia
        /// </summary>
        /// <param name="name"></param>
        /// <param name="agenciaId"></param>
        /// <returns>Registro de InfoSMS</returns>
        Task<InfoSMS> GetInfoSMSlByNameAndAgenciaId (string name, int agenciaId);
        /// <summary>
        /// Interfaz para la obtención de todos los registro de una agencia
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Lista de registros de InfoSMS</returns>
        Task<IEnumerable<InfoSMS>> GetAllInfoSMSByAgencia (int id);
        /// <summary>
        /// Interfaz para la actualización de un SMS
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="agenciaId"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> UpdateSMSTeams (int id, UpdateInfoSMSRequest model, int agenciaId);
    }
}

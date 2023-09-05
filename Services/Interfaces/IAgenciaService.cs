using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.EntityFramework.Models.Agencias;
using Mensajeria_Linux.EntityFramework.Models.InfoWhatsApp;

namespace Mensajeria_Linux.Services.Interfaces
{
    /// <summary>
    /// Interfaz de la capa servicios de Agencia
    /// </summary>
    public interface IAgenciaService
    {
        /// <summary>
        /// Interfaz para la creación de una Agencia
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> CreateAgencia (CreateAgenciaRequest model);
        /// <summary>
        /// Interfaz para la actualización de una Agencia
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> UpdateAgencia ( UpdateAgenciaRequest model);
        /// <summary>
        /// Interfaz para la eliminación de una Agencia
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> DeleteAgencia (string name, int id);
        /// <summary>
        /// Interfaz para obtener una agencia por el nombre
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Agencia</returns>
        Task<Agencia> GetAgenciaByName (string name);
        /// <summary>
        /// Interfaz para obtener una agencia por nombre e id de administrador
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns>Agencia</returns>
        Task<Agencia> GetAgenciaByNameAndAdministradorId (string name,int id);
        /// <summary>
        /// Interfaz para obtener todas las agencias por identificador del administrador
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Lista de Agencias</returns>
        Task<IEnumerable<Agencia>> GetAllAgenciaByAdministradorId (int id );
        /// <summary>
        /// Interfaz para obtener Agencia mediante Nombre y token
        /// </summary>
        /// <param name="name"></param>
        /// <param name="token"></param>
        /// <returns>Agencia</returns>
        Task<Agencia> GetAgenciaByNameAndToken (string name, string token);
        /// <summary>
        /// Interfaz para obtener identificador de agencia mediante nombre y token
        /// </summary>
        /// <param name="name"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<int> GetAgenciaIdByNameAndToken (string name, string token);
    }

}

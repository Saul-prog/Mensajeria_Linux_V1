using AutoMapper;
using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.EntityFramework.Models.Agencias;
using Mensajeria_Linux.Services.Interfaces;

namespace Mensajeria_Linux.Business.Interfaces
{
    /// <summary>
    /// Interfaz para la capa business de Agencia
    /// </summary>
    public interface IAgenciaBusiness
    {
        /// <summary>
        /// Interfaza para la creaciónd de una agencia
        /// </summary>
        /// <param name="model"></param>
        /// <param name="email"></param>
        /// <param name="tokenAdmin"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> CreateAgencia (CreateAgenciaRequest model, string email, string tokenAdmin);
        /// <summary>
        /// Interfaz de la eliminación de una agencia
        /// </summary>
        /// <param name="name"></param>
        /// <param name="adminMail"></param>
        /// <param name="adminToken"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> DeleteAgencia (string name, string adminMail, string adminToken);
        /// <summary>
        /// Interfaz de actualizar una agencia
        /// </summary>
        /// <param name="model"></param>
        /// <param name="adminMail"></param>
        /// <param name="adminToken"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> UpdateAgencia (UpdateAgenciaRequest model, string adminMail, string adminToken);
        /// <summary>
        /// Interfaz de obtener agencia por el nombre de la agencia
        /// </summary>
        /// <param name="name"></param>
        /// <param name="adminMail"></param>
        /// <param name="adminToken"></param>
        /// <returns>Datos de la agencia</returns>
        Task<Agencia> GetAgenciaByName (string name, string adminMail, string adminToken);
        /// <summary>
        /// Interfaz de obtener todas las agencias
        /// </summary>
        /// <param name="adminMail"></param>
        /// <param name="adminToken"></param>
        /// <returns>Lista de Agencias</returns>
        Task<IEnumerable<Agencia>> GetAllAgencias (string adminMail, string adminToken);
    }
}

using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.EntityFramework.Models.Plantillas;

namespace Mensajeria_Linux.Business.Interfaces
{
    /// <summary>
    /// Interfaz de la capa businees de Plantilla
    /// </summary>
    public interface IPlantillaBusiness
    {
        /// <summary>
        /// Interfaz para crear un registro de una plantilla comprobando que sea una agencia o administrador
        /// </summary>
        /// <param name="model"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> CreatePlantilla (CreatePlantillaRequest model, string adminEmail, string adminToken, string agenciaNombre, string agenciaToken);
        /// <summary>
        /// Interfaz para obtiene los registros de plantillas que tiene una agencia comprobando que sea agencia o administrador
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <returns>Lista de los registros plantilla</returns>
        Task<IEnumerable<Plantillas>> GetAllPlantillasByName (string nombre, string? adminEmail, string? adminToken, string agenciaNombre, string? agenciaToken);
        /// <summary>
        /// Interfaz para actualiza una plantilla de una agencia comprobando que sea agencia o administrador
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        /// <exception cref="Exception">No existe la agencia</exception>
        Task<int> UpdatePlantilla (int id, UpdatePlantillaRequest model, string adminEmail, string adminToken, string agenciaNombre, string agenciaToken);
        /// <summary>
        /// Interfaz para elimina una plantilla de una agencia  comprobando que sea agencia o administrador
        /// </summary>
        /// <param name="id"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        /// <exception cref="Exception">No existe Agencia</exception>
        Task<int> DeletePlantilla (int id,  string adminEmail, string adminToken, string agenciaNombre, string agenciaToken);
    }
}

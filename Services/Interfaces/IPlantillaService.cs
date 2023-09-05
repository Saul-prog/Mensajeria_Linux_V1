using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.EntityFramework.Models.Plantillas;

namespace Mensajeria_Linux.Services.Interfaces
{
    /// <summary>
    /// Interfaz de la capa servicio de plantilla
    /// </summary>
    public interface IPlantillaService
    {
        /// <summary>
        /// Interfaz para la creación de una plantilla
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> CreatePlantilla (CreatePlantillaRequest model, int id);
        /// <summary>
        /// Interfaz para la eliminación de una plantilla
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> DeletePlantillaById (int id);
        /// <summary>
        /// Interfaz para la actualización de una plantilla
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> UpdatePlantilla (int id, UpdatePlantillaRequest model);
        /// <summary>
        /// Interfaz para la obtención de todas las plantillas de una agencia
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Lista de registros de Plantilla</returns>
        Task<IEnumerable<Plantillas>> GetAllPlantillasById (int id);
        /// <summary>
        /// Interfaz para obtener la plantilla lista para usar mediante el nombre, la extensión y la agencia
        /// </summary>
        /// <param name="name"></param>
        /// <param name="extension"></param>
        /// <param name="agenciaId"></param>
        /// <returns>String de la plantilla</returns>
        Task<string> GetContenidoPlantillaByNameAndExtensionAndAgenciaId (string name, string extension, int agenciaId);
    }
}

using Mensajeria_Linux.Controllers.Email.Models;
using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.EntityFramework.Models.InfoEmail;

namespace Mensajeria_Linux.Services.Interfaces
{
    /// <summary>
    /// Interfaz del servicio de Email
    /// </summary>
    public interface IInfoEmailService
    {
        /// <summary>
        /// Interfaz para la creación de una Email
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> CreateInfoEmail (CreateInfoEmailRequest model, int id);
        /// <summary>
        /// Interfaz para la eliminación de una Email
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agenciaId"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> DeleteInfoEmail (int id, int agenciaId);
        /// <summary>
        /// Interfaz para la obtención de todos los email de una agencia
        /// </summary>
        /// <param name="agenciaId"></param>
        /// <returns>Lista de Emails de una agencia</returns>
        Task<IEnumerable<InfoEmail>> GetAllInfoEmailByAgencia (int agenciaId);
        /// <summary>
        /// Interfaz para la actualización de un email de una agencia
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="agenciaId"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> UpdateEmailTeams (int id, UpdateInfoEmailRequest model, int agenciaId);
        /// <summary>
        /// Interfaz apra obtener un email por id y agencia
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agenciaId"></param>
        /// <returns>Un registro Email</returns>
        Task<InfoEmail> GetInfoEmailById (int id, int agenciaId);
        /// <summary>
        /// Interfaz para obtener información de un email por nombre y agencia
        /// </summary>
        /// <param name="agenciaId"></param>
        /// <param name="emailOrigen"></param>
        /// <returns>Un registro de Email</returns>
        Task<InfoEmail> GetInfoEmailByAgenciaIdAndTipo (int agenciaId, string emailOrigen);
        /// <summary>
        /// Interfaz para enviar un email con los datos ya rellenos
        /// </summary>
        /// <param name="plantilla"></param>
        /// <param name="infoEmail"></param>
        /// <param name="emailsDestino"></param>
        /// <param name="titulo"></param>
        /// <param name="ficheros"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        int EnviarEmailConPlantillaADestino (string plantilla, InfoEmail infoEmail, List<DatosEmail> emailsDestino, string titulo, List<DatosFichero> ficheros);
    }
}

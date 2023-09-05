using Mensajeria_Linux.Controllers.Email.Models;
using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.EntityFramework.Models.InfoEmail;

namespace Mensajeria_Linux.Business.Interfaces
{
    /// <summary>
    /// Interfaz para la capa business de Email
    /// </summary>
    public interface IEmailBusiness
    {
        /// <summary>
        /// Interfaza para la creaciónd de un Email
        /// </summary>
        /// <param name="model"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> CreateInfoEmail (CreateInfoEmailRequest model, string adminEmail,string adminToken);
        /// <summary>
        /// Interfaz para la obtención de todos los datos de Email de una agencia
        /// </summary>
        /// <param name="agencia"></param>
        /// <param name="token"></param>
        /// <param name="emailAdmin"></param>
        /// <param name="tokenAdmin"></param>
        /// <returns>Lista de datos de una agencia</returns>
        Task<IEnumerable<InfoEmail>> GetAllInfoEmail (string agencia, string? token, string? emailAdmin, string? tokenAdmin);
        /// <summary>
        /// Interfaz para obtener los datos de un email de una agencia
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agenciaName"></param>
        /// <param name="token"></param>
        /// <param name="emailAdmin"></param>
        /// <param name="tokenAdmin"></param>
        /// <returns>Datos de un email</returns>
        Task<InfoEmail> GetInfoEmailById (int id, string agenciaName, string? token, string? emailAdmin, string? tokenAdmin);
        /// <summary>
        /// Interfaz para obtener todos los emails de una agencia
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="emailAdmin"></param>
        /// <param name="tokeAdmin"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> UpdateInfoEmail (int id, UpdateInfoEmailRequest model, string emailAdmin,string tokeAdmin);
        /// <summary>
        /// Interfaz para elimiar los datos de un email
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agencia"></param>
        /// <param name="token"></param>
        /// <param name="emailAdmin"></param>
        /// <param name="tokenAdmin"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> DeleteInfoEmail (int id, string agencia,string? token, string? emailAdmin, string? tokenAdmin);
        /// <summary>
        /// Interfaz para enviar un Email
        /// </summary>
        /// <param name="datos"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> EnviarProcesoAutorizacion (EnviarEmail datos);
    }
}

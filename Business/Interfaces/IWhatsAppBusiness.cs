using Mensajeria_Linux.Controllers.WhatsApp.Models;
using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.EntityFramework.Models.InfoWhatsApp;

namespace Mensajeria_Linux.Business.Interfaces
{
    /// <summary>
    /// Interfaz para la capa business de WhatsApp
    /// </summary>
    public interface IWhatsAppBusiness
    {
        /// <summary>
        /// Interfaz apra crear un registro de WhatsApp
        /// </summary>
        /// <param name="model"></param>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> CreateInfoWhatsApp (CreateInfoWhatsAppRequest model, string agenciaNombre,string agenciaToken, string adminEmail,string adminToken);
        /// <summary>
        /// Interfaz para eliminar un registro de WhatsApp
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
        Task<int> DeleteInfoWhatsApp (int id, string agenciaNombre, string agenciaToken, string adminEmail, string adminToken);
        /// <summary>
        /// Interfaz para obtener todos los registros de WhatsApp de una agencia
        /// </summary>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <returns>Lista de registros de WhatsApp</returns>
        Task<IEnumerable<InfoWhatsApp>> GetAllInfoWhatsApp ( string agenciaNombre, string agenciaToken, string adminEmail, string adminToken);
        /// <summary>
        /// Interfaz para actualizar un registro de WhatsApp
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> UpdateInfoWhatsApp (int id, UpdateInfoWhatsAppRequest model, string agenciaNombre, string agenciaToken, string adminEmail, string adminToken);
        /// <summary>
        /// Interfaz para obtener un registro de WhatsApp de una agencia
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <returns>Un registro de WhatsApp</returns>
        Task<InfoWhatsApp> GetInfoWhatsAppById (int id, string agenciaNombre, string agenciaToken, string adminEmail, string adminToken);
        /// <summary>
        /// Interfaz para enviar WhatsApp
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> Enviar (EnviarWhatsApp model);
    }
}

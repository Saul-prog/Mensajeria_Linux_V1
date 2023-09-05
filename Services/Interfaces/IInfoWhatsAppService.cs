using Microsoft.EntityFrameworkCore;
using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.EntityFramework.Models.InfoWhatsApp;

namespace Mensajeria_Linux.Services.Interfaces
{
    /// <summary>
    /// Interfaz de la capa servicios de WhatsApp
    /// </summary>
    public interface IInfoWhatsAppService
    {
        /// <summary>
        /// Interfaz para la creación de un registro WhatsApp
        /// </summary>
        /// <param name="model"></param>
        /// <param name="agenciaId"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> CreateInfoWhatsApp (CreateInfoWhatsAppRequest model, int agenciaId);
        /// <summary>
        ///  Interfaz para la eliminación de un registro WhatsApp
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agenciaId"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> DeleteInfoWhatsApp (int id, int agenciaId);
        /// <summary>
        /// Interfaz para obtener todos los registros WhatsApp de una agencia
        /// </summary>
        /// <param name="agenciaId"></param>
        /// <returns>Lista de registros WhatsApp</returns>
        Task<IEnumerable<InfoWhatsApp>> GetAllInfoWhatsApp (int agenciaId);
        /// <summary>
        /// Interfaz para la actualización de un registro WhatsApp
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="agenciaId"></param>
        /// <returns></returns>
        Task<int> UpdateInfoWhatsApp (int id, UpdateInfoWhatsAppRequest model,int agenciaId);
        /// <summary>
        /// Interfaz para la obtención de un registro de WhatsApp por id y agencia
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agenciaId"></param>
        /// <returns>Registro WhatsApp</returns>
        Task<InfoWhatsApp> GetInfoWhatsAppById (int id, int agenciaId);
        /// <summary>
        /// Interfaz para la obtención de un registro de WhatsApp por nombre y agencia
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="agenciaId"></param>
        /// <returns>Un registro de InfoWhtasApp</returns>
        Task<InfoWhatsApp> GetInfoWhatsAppByName (string nombre, int agenciaId);
        /// <summary>
        /// Intefaz para la obtención de Json para enviar a WhatsApp
        /// </summary>
        /// <returns>String con la plantilla de envío de mensajes de WhatsApp</returns>
        string GetJsonWhatsApp ( );
        /// <summary>
        /// Interfaz para la obtención de URL destino
        /// </summary>
        /// <returns>String con la url que se va a enviar el WhatsApp</returns>
        string GetURLWhatsApp ( );
    }
}

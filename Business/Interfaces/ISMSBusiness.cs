using Mensajeria_Linux.Controllers.SMSC.Models;
using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.EntityFramework.Models.InfoSMS;

namespace Mensajeria_Linux.Business.Interfaces
{
    /// <summary>
    /// Interfaz de la capa business de SMS
    /// </summary>
    public interface ISMSBusiness
    {
        /// <summary>
        /// Interfaz para obtiener todos los registros de SMS de una agencia comprobando si es agencia, administrador y puede usar SMS
        /// </summary>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <returns>Lista de registros SMS</returns>
        Task<IEnumerable<InfoSMS>> GetAllSMS (string agenciaNombre, string agenciaToken, string adminEmail, string adminToken);
        /// <summary>
        /// interfaz para obtien el registo de SMS de una agencia comprobando si es agencia, administrador y puede usar SMS
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <returns>Registro SMS</returns>
        Task<InfoSMS> GetSMSByNombre (int id, string agenciaNombre, string agenciaToken, string adminEmail, string adminToken);
        /// <summary>
        /// Interfaz para crear un registro de SMS de una agencia comprobando si es agencia, administrador y puede usar SMS
        /// </summary>
        /// <param name="model"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> CreateSMS (CreateInfoSMSRequest model, string adminEmail, string adminToken);
        /// <summary>
        /// Interfaz para actualiza un registro de SMS de una agencia comprobando si es agencia, administrador y puede usar SMS
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        Task<int> UpdateSMS (int id, UpdateInfoSMSRequest model, string adminEmail, string adminToken);
        /// <summary>
        /// Interfaz para eliminar un registro de SMS de una agencia comprobando si es agencia, administrador y puede usar SMS
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
        Task<int> DeleteSMS (int id, string agenciaNombre, string agenciaToken, string adminEmail, string adminToken);
        /// <summary>
        /// Interfaz para enviar un SMS obteniendo los datos para hacerlo, rellenando la plantilla y comprobando si es agencia o admin y si puede usar SMS
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Enviar (EnviarSMS model);
    }
}

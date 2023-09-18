using Amazon.Runtime;
using Amazon.SimpleNotificationService.Model;
using Amazon.SimpleNotificationService;
using Mensajeria_Linux.Business.Interfaces;
using Mensajeria_Linux.Controllers.SMSC.Models;
using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.EntityFramework.Models.InfoSMS;
using Mensajeria_Linux.Services.Interfaces;
using Amazon;

namespace Mensajeria_Linux.Business
{
    /// <summary>
    /// Capa businees de SMS
    /// </summary>
    public class SMSBusiness : ISMSBusiness
    {
        private ISMSService _SMSService;
        private IAgenciaService _genciaService;
        private IAdministradoresService _administradoresService;
        private IPlantillaService _plantillaService;
        /// <summary>
        /// Constructor de la capa business de SMS
        /// </summary>
        /// <param name="sMSService"></param>
        /// <param name="administradoresService"></param>
        /// <param name="agenciaService"></param>
        /// <param name="plantillaService"></param>
        public SMSBusiness (ISMSService sMSService, IAdministradoresService administradoresService, IAgenciaService agenciaService, IPlantillaService plantillaService)
        {
            _SMSService = sMSService;
            _administradoresService = administradoresService;
            _genciaService = agenciaService;
            _plantillaService = plantillaService;
        }
        /// <summary>
        /// Obtiene todos los registros de SMS de una agencia comprobando si es agencia, administrador y puede usar SMS
        /// </summary>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <returns>Lista de registros SMS</returns>
        public async Task<IEnumerable<InfoSMS>> GetAllSMS (string agenciaNombre, string agenciaToken, string adminEmail, string adminToken)
        {
            return await _SMSService.GetAllInfoSMSByAgencia(await GetIdAgencia(adminEmail, adminToken, agenciaNombre, agenciaToken));
        }
        /// <summary>
        /// Obtien el registo de SMS de una agencia comprobando si es agencia, administrador y puede usar SMS
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <returns>Registro SMS</returns>
        public async Task<InfoSMS> GetSMSByNombre (int id, string agenciaNombre, string agenciaToken, string adminEmail, string adminToken)
        {
            return await _SMSService.GetInfoSMSlById(id, await GetIdAgencia(adminEmail, adminToken, agenciaNombre, agenciaToken));
        }
        /// <summary>
        /// Crea un registro de SMS de una agencia comprobando si es agencia, administrador y puede usar SMS
        /// </summary>
        /// <param name="model"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async Task<int> CreateSMS (CreateInfoSMSRequest model, string adminEmail, string adminToken)
        {
            return await _SMSService.CreateInfoSMS(model, await GetIdAgencia(adminEmail, adminToken, model.nombreAgencia, model.tokenAgencia));
        }
        /// <summary>
        /// Actualiza un registro de SMS de una agencia comprobando si es agencia, administrador y puede usar SMS
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async Task<int> UpdateSMS (int id, UpdateInfoSMSRequest model, string adminEmail, string adminToken)
        {
            return await _SMSService.UpdateSMSTeams(id, model, await GetIdAgencia(adminEmail, adminToken, model.nombreAgencia, model.tokenAgencia));
        }
        /// <summary>
        /// Elimina un registro de SMS de una agencia comprobando si es agencia, administrador y puede usar SMS
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
        public async Task<int> DeleteSMS (int id, string agenciaNombre, string agenciaToken, string adminEmail, string adminToken)
        {
            return await _SMSService.DeleteSMS(id, await GetIdAgencia(adminEmail, adminToken, agenciaNombre, agenciaToken));
        }
        /// <summary>
        /// Envía un SMS obteniendo los datos para hacerlo, rellenando la plantilla y comprobando si es agencia o admin y si puede usar SMS
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Enviar (EnviarSMS model)
        {
            int agenciaId = await GetIdAgencia(model.adminEmail, model.adminToken, model.agenciaNombre, model.agenciaToken);
            InfoSMS infoSMS = await _SMSService.GetInfoSMSlByNameAndAgenciaId(model.nombreSMS, agenciaId);
            string plantilla = await _plantillaService.GetContenidoPlantillaByNameAndExtensionAndAgenciaId(model.plantilla, "PLANA", agenciaId);
            plantilla = plantilla.Replace("{identificador}", model.mensaje);
            foreach (var numero in model.numero)
            {
                try
                {
                    var awsCredentials = new BasicAWSCredentials(infoSMS.awsAcceskey, infoSMS.awsSecretKey);
                    using AmazonSimpleNotificationServiceClient client = new(awsCredentials, RegionEndpoint.EUCentral1);
                    PublishResponse response = await client.PublishAsync(new PublishRequest
                    {
                        Message = plantilla,
                        PhoneNumber = numero
                    });   
                    
                }
                catch (Exception ex)
                {
                    return 1;
                }
            }
            return 0;
        }





        /// <summary>
        /// Comprueba que sea agencia, administrador y que pueda usar SMS
        /// </summary>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <returns>Identificador de la agencia</returns>
        /// <exception cref="Exception">No existe la agencia</exception>
        /// <exception cref="Exception">La agencia no puede usar SMS</exception>
        private async Task<int> GetIdAgencia (string adminEmail, string adminToken, string agenciaNombre, string agenciaToken)
        {
            int agenciaId = 0;
            bool esAdmin = await _administradoresService.IsAdministrador(adminEmail, adminToken);
            if (esAdmin)
            {
                Agencia agencia = await _genciaService.GetAgenciaByName(agenciaNombre);
                agenciaId = agencia.id;
                if (!agencia.puedeSMS)
                {
                    throw new Exception("La agencia no puede usar SMS");
                }
            }
            else
            {
                Agencia agencia = await _genciaService.GetAgenciaByNameAndToken(agenciaNombre, agenciaToken);
                agenciaId = agencia.id;
                if (!agencia.puedeSMS)
                {
                    throw new Exception("La agencia no puede usar SMS");
                }
            }
            if (agenciaId == 0)
            {
                throw new Exception("No existe la agencia");
            }
            return agenciaId;
        }

    }
}

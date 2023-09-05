
using Mensajeria_Linux.Business.Interfaces;
using Mensajeria_Linux.Controllers.SMSC.Models;
using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.EntityFramework.Models.InfoSMS;
using Microsoft.AspNetCore.Mvc;

namespace Mensajeria_Linux.Controllers.SMSC
{
    /// <summary>
    /// Controlador para SMS
    /// </summary>
    [ApiController]
    [Route("{version:apiVersion}/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class SMSController
    {
        private ISMSBusiness _SMSBusiness;
        /// <summary>
        /// Constructor del controlador para SMS
        /// </summary>
        /// <param name="sMSBusiness"></param>
        public SMSController (ISMSBusiness sMSBusiness)
        {
            _SMSBusiness = sMSBusiness;
        }
        /// <summary>
        /// Crea los datos necesarios para enviar un SMS
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        ///          Status 201 si ha se ha hecho correctamente
        ///          Status 500 si ha ocurrido algún error
        /// </returns>
        [HttpPost("Crear")]
        public async Task<ActionResult> CreateSMS (CrearDatosSMS model)
        {
            CreateInfoSMSRequest infoSMS = model.sMSRequest;
            int infoSMSId = await _SMSBusiness.CreateSMS(infoSMS, model.adminEmail, model.adminToken);

            if (infoSMSId != 0)
            {
                return new ObjectResult("Creado correctamente")
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            return new ObjectResult("InfoTeams no se ha creado")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

        }

        /// <summary>
        /// Devuelve la lista de los datos SMS por agencia
        /// </summary>
        /// <param name="agencia"></param>
        /// <param name="token"></param>
        /// <param name="emailAdmin"></param>
        /// <param name="tokenAdmin"></param>
        /// <returns>Lista de InfoSMS</returns>
        [HttpGet("{agencia}")]
        public async Task<IActionResult> GetAllSMSByAgencia (string agencia, string? token, string? emailAdmin, string? tokenAdmin)
        {
            IEnumerable<InfoSMS> infoSMS = await _SMSBusiness.GetAllSMS(agencia, token, emailAdmin, tokenAdmin);

            return new ObjectResult(infoSMS)
            {
                StatusCode = StatusCodes.Status200OK
            };
        }
        /// <summary>
        /// Elimina un registro de datos de SMS perteneciente a una agencia
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agencia"></param>
        /// <param name="token"></param>
        /// <param name="emailAdmin"></param>
        /// <param name="tokenAdmin"></param>
        /// <returns>
        ///          Status 200 si ha se ha hecho correctamente
        ///          Status 500 si ha ocurrido algún error
        /// </returns>
        [HttpDelete("Eliminar")]
        public async Task<IActionResult> DeleteSMS (int id, string agencia, string? token, string? emailAdmin, string? tokenAdmin)
        {
            int deveulto = await _SMSBusiness.DeleteSMS(id, agencia, token, emailAdmin, tokenAdmin);
            if (deveulto == 0)
            {
                return new ObjectResult("No se ha eliminado correctamente")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            return new ObjectResult("Se ha eliminado correctamente")
            {
                StatusCode = StatusCodes.Status200OK
            };
        }
        /// <summary>
        /// Actualiza el registro de los datos de un SMS perteneciente a una agencia
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>
        ///          Status 200 si ha se ha hecho correctamente
        ///          Status 500 si ha ocurrido algún error
        /// </returns>
        [HttpPut("Actualizar")]
        public async Task<IActionResult> UpdateSMS (int id, ActualizarSMS model)
        {
            UpdateInfoSMSRequest infoSMS = model.SMSRequest;
            int deveulto = await _SMSBusiness.UpdateSMS(id, infoSMS, model.adminEmail, model.adminToken);
            if (deveulto == 0)
            {
                return new ObjectResult("No se ha actualizado correctamente")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            return new ObjectResult("Se ha actualizado correctamente")
            {
                StatusCode = StatusCodes.Status200OK
            };
        }
        /// <summary>
        /// Envía un SMS a una lista de números
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        ///          Status 200 si ha se ha hecho correctamente
        ///          Status 500 si ha ocurrido algún error
        /// </returns>
        [HttpPost("Enviar")]
        public async Task<IActionResult> EnviarSMS (EnviarSMS model)
        {
            int deveulto = await _SMSBusiness.Enviar(model);
            if (deveulto == 0)
            {
                return new ObjectResult("No se ha actualizado correctamente")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            return new ObjectResult("Se ha actualizado correctamente")
            {
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
}


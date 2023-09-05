using Microsoft.AspNetCore.Mvc;
using Mensajeria_Linux.Business.Interfaces;
using Mensajeria_Linux.Controllers.Email.Models;
using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.EntityFramework.Models.InfoEmail;

namespace Mensajeria_Linux.Controllers.Email
{
    /// <summary>
    /// Controlador de Emails
    /// </summary>
    [ApiController]
    [Route("{version:apiVersion}/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class EmailController
    {

        private IEmailBusiness _infoEmailBusiness;
        /// <summary>
        /// Constructor del controlador de Emails
        /// </summary>
        /// <param name="infoEmailBusiness"></param>
        public EmailController(IEmailBusiness infoEmailBusiness)
        {
            _infoEmailBusiness = infoEmailBusiness;
        }
        /// <summary>
        /// Crea los datos necesarios para enviar un Email
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        ///          Status 200 si ha se ha hecho correctamente
        ///          Status 500 si ha ocurrido algún error
        /// </returns>
        [HttpPost("Crear")]
        public async Task<ActionResult> CreateInfoEmail (CrearDatosEmail model)
        {
            CreateInfoEmailRequest infoEmail = new CreateInfoEmailRequest
            {
                nombreAgencia =model.nombreAgencia,
                tokenAgencia=model.tokenAgencia,
                emailOrigen=model.emailOrigen,
                host = model.host,
                port= model.port,
                emailTokenPassword = model.emailToken,
                emailNombre=model.nombreEmail,
            };
            int infoEmailId = await _infoEmailBusiness.CreateInfoEmail(infoEmail,model.emailAdmin, model.tokenAdmin);

            if (infoEmailId != 0)
            {
                return new ObjectResult("Creado correctamente")
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            return new ObjectResult("Los datos del email no se ha creado")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

        }
        /// <summary>
        /// Devuelve todos los datos de envío de Email de una agencia
        /// </summary>
        /// <param name="agencia"></param>
        /// <param name="token"></param>
        /// <param name="emailAdmin"></param>
        /// <param name="tokenAdmin"></param>
        /// <returns name="InfoEmail">Lista de InfoEmail,puede ser vacía </returns>
        [HttpGet("{agencia}")]
        public async Task<IActionResult> GetAllInfoEmailByAgencia (string agencia, string? token, string? emailAdmin, string? tokenAdmin)
        {
            IEnumerable<InfoEmail> infoEmail = await _infoEmailBusiness.GetAllInfoEmail(agencia,token, emailAdmin, tokenAdmin);

            return new ObjectResult(infoEmail)
            {
                StatusCode = StatusCodes.Status200OK
            };
        }
        /// <summary>
        /// Elimina los datos de envío de un email
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
        public async Task<IActionResult> DeleteIfoEmail (int id, string agencia, string? token, string? emailAdmin, string? tokenAdmin)
        {
            int deveulto = await _infoEmailBusiness.DeleteInfoEmail(id, agencia, token, emailAdmin, tokenAdmin);
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
        /// Se actualiza los datos de envío de un email
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model">ActualizarDatosEmail</param>
        /// <returns>
        ///          Status 200 si ha se ha hecho correctamente
        ///          Status 500 si ha ocurrido algún error
        /// </returns>
        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> UpdateInfoEmail (int id, ActualizarDatosEmail model)
        {
            UpdateInfoEmailRequest infoEmail = new UpdateInfoEmailRequest
            {
                emailOrigen=model.emailOrigen,
                host=model.host,
                nombreAgencia=model.nombreAgencia,
                emailNombre = model.nombreEmail,
                port=model.port,
                tokenAgencia=model.tokenAgencia,
                tokenEmail = model.emailToken
            };
            int deveulto = await _infoEmailBusiness.UpdateInfoEmail(id, infoEmail,model.emailAdmin,model.tokenAdmin);
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
        /// Envía un Email según la agencia, plantilla, datos para la plantilla y los correos destino
        /// </summary>
        /// <param name="datos"></param>
        /// <returns>
        ///          Status 200 si ha se ha hecho correctamente
        ///          Status 500 si ha ocurrido algún error
        /// </returns>
        [HttpPost("Enviar")]
        public async Task<IActionResult> SendEmail (EnviarEmail datos)
        {
            switch(datos.plantilla)
            {
                case "ProcesoAutorizacion":
                   
                    if(await _infoEmailBusiness.EnviarProcesoAutorizacion(datos) != 0)
                    {
                        return new ObjectResult("Se ha enviado correctamente")
                        {
                            StatusCode = StatusCodes.Status200OK
                        };
                    }
                    else
                    {
                        return new ObjectResult("No se ha enviado correctamente")
                        {
                            StatusCode = StatusCodes.Status500InternalServerError
                        };
                    }
                   
                default:
                    return new ObjectResult("No existe dicha plantilla")
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
            }
        }

    }
}

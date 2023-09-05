using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mensajeria_Linux.Business.Interfaces;
using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.EntityFramework.Models.InfoWhatsApp;
using Mensajeria_Linux.Controllers.WhatsApp.Models;

namespace Mensajeria_Linux.Controllers.WhatsApp
{
    /// <summary>
    /// Controlador de mensajes de WhatsApp
    /// </summary>
    [ApiController]
    [Route("{version:apiVersion}/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class WhatsAppController
    {
        private IWhatsAppBusiness _infoWhatsAppBusiness;

        public WhatsAppController (IWhatsAppBusiness infoWhatsAppBusiness)
        {
            _infoWhatsAppBusiness = infoWhatsAppBusiness;
        }
        /// <summary>
        /// Crea un datos de WhatsApp
        /// </summary>
        /// <param name="model">CreateInfoWhatsAppRequest</param>
        /// <returns>
        ///          Status 201 si ha se ha hecho correctamente
        ///          Status 500 si ha ocurrido algún error
        /// </returns>
        [HttpPost]
        public async Task<ActionResult> CreateInfoTeams (CrearDatosWhatsApp model)
        {
            int infoWhatsAppId = await _infoWhatsAppBusiness.CreateInfoWhatsApp(model.datosWhatsApp, model.agenciaNombre, model.agenciaToken,model.adminEmail,model.adminToken);

            if (infoWhatsAppId != 0)
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
        /// Devuelve todos los datos de WhatsApp que hay de una agencia
        /// </summary>
        /// <param></param>
        /// <returns>Lista de Datos sobre WhatsApps</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllInfoTeams ( string agenciaNombre, string? agenciaToken, string? adminEmail, string? adminToken)
        {
            IEnumerable<InfoWhatsApp> infoWhatsApps = await _infoWhatsAppBusiness.GetAllInfoWhatsApp(agenciaNombre, agenciaToken, adminEmail, adminToken);

            return new ObjectResult(infoWhatsApps)
            {
                StatusCode = StatusCodes.Status200OK
            };
        }
        /// <summary>
        /// Método para eliminar un registro de InfoWhatsApp por id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <returns>
        ///          Status 201 si ha se ha hecho correctamente
        ///          Status 500 si ha ocurrido algún error
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIfoteams (int id, string agenciaNombre, string? agenciaToken, string? adminEmail, string? adminToken)
        {
            int deveulto = await _infoWhatsAppBusiness.DeleteInfoWhatsApp(id,agenciaNombre,agenciaToken,adminEmail,adminToken);
            if (deveulto != 0)
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
        /// Controlador para actualizar un registro de InfoWhatsApp
        /// </summary>
        /// <param name="id">Identificador del que se quiere actualizar</param>
        /// <param name="infoWhatsApp">ActualizarDatosWhatsApp</param>
        /// <returns>
        ///          Status 200 si ha se ha hecho correctamente
        ///          Status 500 si ha ocurrido algún error
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInfoTeams (int id, ActualizarDatosWhatsApp infoWhatsApp)
        {
            int deveulto = await _infoWhatsAppBusiness.UpdateInfoWhatsApp(id, infoWhatsApp.datosActualizar,
                infoWhatsApp.agenciaNombre,infoWhatsApp.agenciaToken,infoWhatsApp.adminEmail,infoWhatsApp.adminToken);
            if (deveulto != 0)
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
        /// Envía un WhatsApp a los números solicitados
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Enviar")]
        public async Task<IActionResult> EnviarWhatsApp (EnviarWhatsApp model)
        {
            int deveulto = await _infoWhatsAppBusiness. Enviar(model);
            if (deveulto != 0)
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

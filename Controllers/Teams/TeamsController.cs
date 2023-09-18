using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mensajeria_Linux.Business.Interfaces;
using Mensajeria_Linux.Controllers.Teams.Models;
using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.EntityFramework.Models.InfoTeam;
using System.Runtime.InteropServices;

namespace Mensajeria_Linux.Controllers.Teams
{
    /// <summary>
    /// Controlador de mensajes Teams
    /// </summary>
    [ApiController]
    [Route("{version:apiVersion}/[controller]")]    
    [Consumes("application/json")]
    [Produces("application/json")]    
    public class TeamsController
    {
        private ITeamsBusiness _infoTeamsProvider;
        /// <summary>
        /// Constructor del controlador de mensajes Teams
        /// </summary>
        /// <param name="infoTeamsProvider"></param>
        public TeamsController(ITeamsBusiness infoTeamsProvider  )
        {
            _infoTeamsProvider = infoTeamsProvider;
        }

        /// <summary>
        /// Crear los deatos de Teams
        /// </summary>
        /// <param name="model">CrearDatosTeams</param>
        /// <returns>
        ///          Status 201 si ha se ha hecho correctamente
        ///          Status 500 si ha ocurrido algún error
        /// </returns>
        [HttpPost("Crear")]
        public async Task<ActionResult> CreateInfoTeams(CrearDatosTeams model)
        {
            int infoTeamsId = await _infoTeamsProvider.CreateInfoTeams(model.model, model.adminEmail,model.adminToken);

            if (infoTeamsId != 0)
            {
                return new ObjectResult("Creado correctamente")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            return new ObjectResult("The InfoTeams was not created in the database.")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

        }
        /// <summary>
        /// Se devuelven todos datos de Teams de una agencia
        /// </summary>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <returns>Lista de InfoTeams</returns>
        [HttpGet("Ver")]
        public async Task<IActionResult> GetAllInfoTeams (string agenciaNombre, string? agenciaToken, string? adminEmail, string? adminToken)
        {
            IEnumerable<InfoTeams> infoTeams =  await _infoTeamsProvider.GetAllInfoTeams( agenciaNombre,  agenciaToken,  adminEmail,  adminToken);
            
                return new ObjectResult(infoTeams)
                {
                    StatusCode = StatusCodes.Status200OK
                };

        }
        /// <summary>
        /// Elimina un registro de Teams de una agencia
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <returns>
        ///          Status 200 si ha se ha hecho correctamente
        ///          Status 500 si ha ocurrido algún error
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIfoteams (int id, string agenciaNombre,string? agenciaToken, string? adminEmail, string? adminToken)
        {
            int deveulto = await _infoTeamsProvider.DeleteInfoTeams(id,agenciaNombre,agenciaToken,adminEmail,adminToken);
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
        /// Actualiza el registro de los datos de un Teams de una agencia
        /// </summary>
        /// <param name="id"></param>
        /// <param name="infoTeams">UpdateInfoTeamsRequest</param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <returns>
        ///          Status 200 si ha se ha hecho correctamente
        ///          Status 500 si ha ocurrido algún error
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInfoTeams(int id, UpdateInfoTeamsRequest infoTeams, string? adminEmail, string? adminToken)
        {
            int deveulto = await _infoTeamsProvider.UpdateInfoTeams(id, infoTeams,adminEmail,adminToken);
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
        /// Envía un mensaje de Teams
        /// </summary>
        /// <param name="model">EnviarTeams</param>
        /// <returns>
        ///          Status 200 si ha se ha hecho correctamente
        ///          Status 500 si ha ocurrido algún error
        /// </returns>
        [HttpPost("Enviar")]
        public async Task<IActionResult> EnviarTeamsConWebHook(EnviarTeams model)
        {
            int deveulto;
            switch (model.plantilla)
            {
                case "ProcesoAutorizacion":
                    deveulto = await _infoTeamsProvider.EnviarTeamsProcesoAutorizacion(model);
                    break;
                default:
                    deveulto = 0;
                    break;
            }
            if (deveulto == 0)
            {
                return new ObjectResult("No se ha enviado correctamente")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            return new ObjectResult("Se ha enviado correctamente")
            {
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mensajeria_Linux.Business.Interfaces;
using Mensajeria_Linux.Controllers.AgenciaC.Models;
using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.EntityFramework.Models.Agencias;

namespace Mensajeria_Linux.Controllers.AgenciaC
{
    /// <summary>
    /// AgenciaController 
    /// </summary>
    [ApiController]
    [Route("{version:apiVersion}/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]   
    public class AgenciaController
    {
        private IAgenciaBusiness _agenciaBusiness;
        /// <summary>
        /// Constructor de Agencia
        /// </summary>
        /// <param name="agenciaBusiness"></param>
        public AgenciaController (IAgenciaBusiness agenciaBusiness)
        {
            _agenciaBusiness = agenciaBusiness;
        }
        ///<summary>
        /// Crea una nueva agencia.
        ///</summary>
        /// <param name="model">Los datos para crear la agencia.</param>
        [HttpPost("Create")]
        public async Task<ActionResult> CreateAgencia (CrearAgencia model)
        {

            CreateAgenciaRequest agencia = new CreateAgenciaRequest
            {
                nombreAgencia = model.nombreAgencia,
                puedeEmail = model.puedeEmail,
                puedeSMS = model.puedeSMS,
                puedeTeams = model.puedeTeams,
                puedeWhatsApp = model.puedeWhatsApp,
                Created = DateTime.Now,
                token = ""
            };

            int agenciaid = await _agenciaBusiness.CreateAgencia(agencia, model.emailAdmin, model.tokenAdmin);

            if (agenciaid != 0)
            {
                return new ObjectResult("Creado correctamente")
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            return new ObjectResult("Agencia no se ha creado")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

        }
        ///<summary>
        /// Actualiza una agencia existente.
        ///</summary>
        /// <param name="model">Los datos para actualizar la agencia. </param>
        [HttpPut("Actualizar")]        
        public async Task<ActionResult> ActualizarAgencia (ActualizarAgencia model)
        {
            UpdateAgenciaRequest agencia = new UpdateAgenciaRequest
            {
                nombreAgencia = model.nombreAgencia,
                puedeEmail = model.puedeEmail,
                puedeSMS = model.puedeSMS,
                puedeTeams = model.puedeTeams,
                puedeWhatsApp = model.puedeWhatsApp,
                Updated = DateTime.Now,
                token = model.tokenAgencia
            };
            int agenciaid = await _agenciaBusiness.UpdateAgencia(agencia, model.emailAdmin, model.tokenAdmin);

            if (agenciaid != 0)
            {
                return new ObjectResult(agenciaid)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            return new ObjectResult("Agencia no se ha actualizado")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
        ///<summary>
        /// Obtiene una lista de agencias o una agencia específica por nombre.
        ///</summary>
        /// <param name="model">Los datos para buscar agencias.</param>
        [HttpGet("Ver")]
        public async Task<IActionResult> VerAgencias (string nombreAgencia,string emailAdmin, string tokenAdmin)
        {            
            if (string.IsNullOrEmpty(nombreAgencia))
            {
                IEnumerable<Agencia> agencia = await _agenciaBusiness.GetAllAgencias(emailAdmin, tokenAdmin);
                if (agencia != null)
                {
                    return new ObjectResult(agencia)
                    {
                        StatusCode = StatusCodes.Status200OK
                    };
                }
                return new ObjectResult("Agencia no se ha encontrado")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            else
            {
                Agencia agencia =  await _agenciaBusiness.GetAgenciaByName(nombreAgencia, emailAdmin, tokenAdmin);
                if (agencia != null)
                {
                    return new ObjectResult(agencia)
                    {
                        StatusCode = StatusCodes.Status200OK
                    };
                }
                return new ObjectResult("Agencia no se ha encontrado")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            
        }
        ///<summary>
        /// Elimina una agencia.
        ///</summary>
        /// <param name="model">Los datos para eliminar la agencia.</param>
        [HttpDelete("Eliminar")]
        public async Task<IActionResult> EliminarAgencia(EliminarAgencia model)
        {
            int deveulto = await _agenciaBusiness.DeleteAgencia(model.nombreAgencia, model.emailAdmin, model.tokenAdmin);
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
    }
}

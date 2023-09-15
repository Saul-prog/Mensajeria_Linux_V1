using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mensajeria_Linux.Business;
using Mensajeria_Linux.Business.Interfaces;
using Mensajeria_Linux.Controllers.PlantillasC.Models;
using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.EntityFramework.Models.InfoEmail;
using Mensajeria_Linux.EntityFramework.Models.Plantillas;
using Microsoft.Extensions.FileProviders;
using static ExpressionAntlrParser;
using System.Text;

namespace Mensajeria_Linux.Controllers.PlantillasC
{
    /// <summary>
    /// Controlador para plantillas
    /// </summary>
    [Route("{version:apiVersion}/[controller]")]
    [ApiController]
    public class PlantillasController
    {
        private IPlantillaBusiness _plantillaBusiness;
        /// <summary>
        /// Constructor de del controlador de plantillas
        /// </summary>
        /// <param name="plantillaBusiness"></param>
        public PlantillasController(IPlantillaBusiness plantillaBusiness)
        {
            _plantillaBusiness = plantillaBusiness;
        }
        /// <summary>
        /// Crea una plantilla con los datos pasados
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        ///          Status 201 si ha se ha hecho correctamente
        ///          Status 500 si ha ocurrido algún error
        /// </returns>
        [HttpPost("Crear")]
        public async Task<ActionResult> CrearPlantilla([FromForm] CrearPlantilla model )
        {
            byte[] fileBytes=null;


            using (var memoryStream = new MemoryStream())
            {
                await model.file.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }
            
            CreatePlantillaRequest plantillaRequest = new CreatePlantillaRequest
                {
                    nombre = model.nombre,
                    plantillaHtml = model.plantillaHtml,
                    plantillaJSON = fileBytes,
                    plantillaPlana = model.plantillaPlana

                };
            
            
            int plantillaId = await _plantillaBusiness.CreatePlantilla(plantillaRequest, model.correoAmin,
                model.tokenAdmin, model.agenciaNombre, model.tokenAgencia);

            if (plantillaId != 0)
            {
                return new ObjectResult("Creado correctamente")
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            return new ObjectResult("Plantilla no creada")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }

        /// <summary>
        /// Se devuelve la lista de las plantillas que tenga una agencia
        /// </summary>
        /// <param name="plantilla"></param>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <returns> Lista de plantillas </returns>
        [HttpGet("{agenciaNombre}")]
        public async Task<IActionResult> GetAllInfoEmailByAgencia (string plantilla, string? email,string? token, string agenciaNombre, string? agenciaToken)
        {
            IEnumerable<Plantillas> plantillas = await _plantillaBusiness.GetAllPlantillasByName(plantilla,email, token, agenciaNombre, agenciaToken);

            return new ObjectResult(plantillas)
            {
                StatusCode = StatusCodes.Status200OK
            };
        }
        /// <summary>
        /// Actualiza la plantilla
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>
        ///          Status 200 si ha se ha hecho correctamente
        ///          Status 500 si ha ocurrido algún error
        /// </returns>
        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> UpdatePlantilla (int id, [FromForm] ActualizarPlantilla model)
        {
            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await model.file.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }
            UpdatePlantillaRequest infoEmail = new UpdatePlantillaRequest
            {
               nombre=model.nombre,
               plantillaHtml=model.plantillaHtml,
               plantillaJSON=fileBytes,
               plantillaPlana= model.plantillaPlana,
            };
            int deveulto = await _plantillaBusiness.UpdatePlantilla( id,infoEmail, model.correoAmin, model.tokenAdmin, model.agenciaNombre,model.tokenAgencia);
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
        /// Elimina una plantilla
        /// </summary>
        /// <param name="id"></param>
        /// <param name="emailAdmin"></param>
        /// <param name="TokenAmin"></param>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <returns>
        ///          Status 200 si ha se ha hecho correctamente
        ///          Status 500 si ha ocurrido algún error
        /// </returns>
        [HttpDelete("Eliminar")]
        public async Task<IActionResult> DeletePlantilla(int id, string? emailAdmin,string? TokenAmin, string agenciaNombre, string? agenciaToken)
        {
            int deveulto = await _plantillaBusiness.DeletePlantilla(id, emailAdmin, TokenAmin, agenciaNombre, agenciaToken);
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

using AutoMapper;
using Mensajeria_Linux.Business.Interfaces;
using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.EntityFramework.Models.InfoEmail;
using Mensajeria_Linux.EntityFramework.Models.Plantillas;
using Mensajeria_Linux.Services;
using Mensajeria_Linux.Services.Interfaces;

namespace Mensajeria_Linux.Business
{
    /// <summary>
    /// Capa business de Plantilla
    /// </summary>
    public class PlantillaBusiness : IPlantillaBusiness
    {
        private IPlantillaService _plantillaService;
        private IAdministradoresService _administradoresService;
        private IAgenciaService _genciaService;
        /// <summary>
        /// Constructor de plantilla
        /// </summary>
        /// <param name="plantillaService">IPlantillaService</param>
        /// <param name="administradoresService">IAdministradoresService</param>
        /// <param name="genciaService">IAgenciaService</param>
        public PlantillaBusiness (IPlantillaService plantillaService, IAdministradoresService administradoresService, IAgenciaService genciaService)
        {
            _plantillaService = plantillaService;
            _administradoresService = administradoresService;
            _genciaService = genciaService; 
        }
        /// <summary>
        /// Crea un registro de una plantilla comprobando que sea una agencia o administrador
        /// </summary>
        /// <param name="model"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async Task<int> CreatePlantilla (CreatePlantillaRequest model, string adminEmail, string adminToken, string agenciaNombre, string agenciaToken)
        {           
            return await _plantillaService.CreatePlantilla(model,await GetIdAgencia(adminEmail,adminToken,agenciaNombre,agenciaToken));            
        }
        /// <summary>
        /// Obtiene los registros de plantillas que tiene una agencia comprobando que sea agencia o administrador
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <returns>Lista de los registros plantilla</returns>
        public async Task<IEnumerable<Plantillas>> GetAllPlantillasByName (string nombre, string? adminEmail, string? adminToken, string agenciaNombre, string? agenciaToken)
        {           
            return await _plantillaService.GetAllPlantillasById(await GetIdAgencia(adminEmail, adminToken, agenciaNombre, agenciaToken));                       
        }
        /// <summary>
        /// Actualiza una plantilla de una agencia comprobando que sea agencia o administrador
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        /// <exception cref="Exception">No existe la agencia</exception>
        public async Task<int> UpdatePlantilla (int id, UpdatePlantillaRequest model, string adminEmail, string adminToken, string agenciaNombre, string agenciaToken)
        {
            if(await GetIdAgencia(adminEmail, adminToken, agenciaNombre, agenciaToken) == 0) 
            {
                throw new Exception("No existe Agencia");
            }
            return await _plantillaService.UpdatePlantilla(id,model);             
        }
        /// <summary>
        /// Elimina una plantilla de una agencia  comprobando que sea agencia o administrador
        /// </summary>
        /// <param name="id"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        /// <exception cref="Exception">No existe Agencia</exception>
        public async Task<int> DeletePlantilla (int id, string? adminEmail, string? adminToken, string agenciaNombre, string? agenciaToken)
        {
            if (await GetIdAgencia(adminEmail, adminToken, agenciaNombre, agenciaToken) == 0)
            {
                throw new Exception("No existe Agencia");
            }
            return await _plantillaService.DeletePlantillaById(id);            
        }

        /// <summary>
        /// Comprueba que sea agencia o administrador 
        /// </summary>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <returns>Identificador de la agencia</returns>
        /// <exception cref="Exception">No existe la agencia</exception>
        private async Task<int> GetIdAgencia (string adminEmail,string adminToken, string agenciaNombre,string agenciaToken)
        {
            int agenciaId = 0;
            bool esAdmin = await _administradoresService.IsAdministrador(adminEmail, adminToken);
            if (esAdmin)
            {
                Agencia agencia = await _genciaService.GetAgenciaByName(agenciaNombre);
                agenciaId=agencia.id;
            }
            else
            {
                agenciaId = await _genciaService.GetAgenciaIdByNameAndToken(agenciaNombre, agenciaToken);
            }
            if(agenciaId == 0)
            {
                throw new Exception("No existe la agencia");
            }
            return agenciaId;
        }
    }
}

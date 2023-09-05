using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.Business.Interfaces;
using Mensajeria_Linux.Services.Interfaces;
using Mensajeria_Linux.EntityFramework.Models.InfoTeam;
using Mensajeria_Linux.Controllers.Teams.Models;
using AdaptiveCards.Templating;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace Mensajeria_Linux.Business
{
    /// <summary>
    /// Capa business de Teams
    /// </summary>
    public class TeamsBusiness : ITeamsBusiness
    {
        private IInfoTeamService _infoTeamService;
        private IAgenciaService _genciaService;
        private IAdministradoresService _administradoresService;
        private IPlantillaService _plantillaService;
        /// <summary>
        /// Constructor de Teams
        /// </summary>
        /// <param name="infoTeamService"></param>
        /// <param name="administradoresService"></param>
        /// <param name="agenciaService"></param>
        /// <param name="plantillaService"></param>
        public TeamsBusiness(IInfoTeamService infoTeamService,  IAdministradoresService administradoresService, IAgenciaService agenciaService, IPlantillaService plantillaService)
        {
            _infoTeamService = infoTeamService;
            _administradoresService = administradoresService;
            _genciaService = agenciaService;
            _plantillaService = plantillaService;
        }
        /// <summary>
        /// Devuelve todos los registros de una agencia comprobando que sea agencia o administrador y pueda usar Teams
        /// </summary>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <returns>Lista de registros de Teams</returns>
        public async Task<IEnumerable<InfoTeams>> GetAllInfoTeams (string agenciaNombre, string agenciaToken, string adminEmail, string adminToken)
        {
            return await _infoTeamService.GetAllInfoTeamsByAgenciaId (await GetIdAgencia(adminEmail, adminToken, agenciaNombre, agenciaToken));
        }
        /// <summary>
        /// Devuelve un registro de una agencia comprobando que sea agencia o administrador y pueda usar Teams
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <returns>Registro de Teams</returns>
        public async Task<InfoTeams> GetInfoTeamsByNombre (string nombre, string agenciaNombre, string agenciaToken, string adminEmail, string adminToken)
        {
            return await _infoTeamService.GetInfoTeamsByNombreAdnAgenciaId(nombre, await GetIdAgencia(adminEmail, adminToken, agenciaNombre, agenciaToken));
        }
        /// <summary>
        /// Crea el registro de un Teams en una agencia comprobando que sea agencia o administrador y pueda usar Teams
        /// </summary>
        /// <param name="model"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async Task<int> CreateInfoTeams (CreateInfoTeamsRequest model, string adminEmail, string adminToken)
        {
           return await _infoTeamService.CreateInfoTeams (model, await GetIdAgencia(adminEmail, adminToken, model.nombreAgencia, model.tokenAgencia));
        }
        /// <summary>
        /// Actualiza los registros de un Teams en una agencia comprobando que sea agencia o administrador y pueda usar Teams
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async Task<int> UpdateInfoTeams (int id, UpdateInfoTeamsRequest model, string adminEmail, string adminToken)
        {        
            return await _infoTeamService.UpdateInfoTeams(id, model, await GetIdAgencia(adminEmail, adminToken, model.nombreAgencia, model.tokenAgencia));
        }
        /// <summary>
        /// Elimina un registro de Teams de una agencia comprobando que sea agencia o administrador y pueda usar Teams
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
        public async Task<int> DeleteInfoTeams (int id, string agenciaNombre, string agenciaToken, string adminEmail, string adminToken)
        {
           return await _infoTeamService.DeleteInfoTeams (id, await GetIdAgencia(adminEmail, adminToken, agenciaNombre, agenciaToken));
        }
        /// <summary>
        /// Envía un mensaje de teams rellenando los datos necesarios para enviarlo, rellenando la plantilla, comprobado si es agencia o administrador y si puede enviar Teams 
        /// </summary>
        /// <param name="teams"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async Task<int> EnviarTeamsProcesoAutorizacion (EnviarTeams teams)
        {
            int agenciaId = 0;
            bool esAdmin = await _administradoresService.IsAdministrador(teams.adminEmail, teams.adminToken);
            if (esAdmin)
            {
                Agencia agencia = await _genciaService.GetAgenciaByName(teams.nombreAgencia);
                agencia.id = agenciaId;
            }
            else
            {
                agenciaId = await _genciaService.GetAgenciaIdByNameAndToken(teams.nombreAgencia, teams.tokenAgencia);
            }
            if (agenciaId != 0)
            {
                InfoTeams infoTeams = await _infoTeamService.GetInfoTeamsByNombreAdnAgenciaId(teams.nombre, agenciaId);
                if (infoTeams == null) { return 0; }
                string plantilla = await _plantillaService.GetContenidoPlantillaByNameAndExtensionAndAgenciaId(teams.plantilla, "JSON", agenciaId);
                if (plantilla == null) { return 0; }
                JObject jplantilla = JObject.Parse(plantilla);
                AdaptiveCardTemplate template = new AdaptiveCardTemplate(jplantilla);
                string cardJson = template.Expand(teams.datos);
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                
                var content = new StringContent(cardJson, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync(infoTeams.webHook, content);
                Console.WriteLine(response.Content);
                return response.IsSuccessStatusCode ? 1 : 0;
            }
            return 0;
       }

        /// <summary>
        /// Comprueba que sea agencia, administrador y que pueda usar Teams
        /// </summary>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <returns>Identificador de la agencia</returns>
        /// <exception cref="Exception">No existe la agencia</exception>
        /// <exception cref="Exception">La agencia no puede usar Teams</exception>
        private async Task<int> GetIdAgencia (string adminEmail, string adminToken, string agenciaNombre, string agenciaToken)
        {
            int agenciaId = 0;
            bool esAdmin = await _administradoresService.IsAdministrador(adminEmail, adminToken);
            if (esAdmin)
            {
                Agencia agencia = await _genciaService.GetAgenciaByName(agenciaNombre);
                agenciaId = agencia.id;
                if (!agencia.puedeTeams)
                {
                    throw new Exception("La agencia no puede usar Teams");
                }
            }
            else
            {
                Agencia agencia = await _genciaService.GetAgenciaByNameAndToken(agenciaNombre, agenciaToken);
                agenciaId = agencia.id;
                if (!agencia.puedeTeams)
                {
                    throw new Exception("La agencia no puede usar Teams");
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

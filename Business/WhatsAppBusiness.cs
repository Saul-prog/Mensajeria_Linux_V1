using Mensajeria_Linux.Business.Interfaces;
using Mensajeria_Linux.Controllers.WhatsApp.Models;
using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.EntityFramework.Models.InfoWhatsApp;
using Mensajeria_Linux.Services.Interfaces;
using System.Text;

namespace Mensajeria_Linux.Business
{
    /// <summary>
    /// Capa Business de WhatsApp
    /// </summary>
    public class WhatsAppBusiness : IWhatsAppBusiness
    {
        private IInfoWhatsAppService _infoWhatsAppService;
        private IAgenciaService _genciaService;
        private IAdministradoresService _administradoresService;
        private IPlantillaService _plantillaService;
        /// <summary>
        /// Constructor de capa business de Whatsapp
        /// </summary>
        /// <param name="infoWhatsAppService"></param>
        /// <param name="agenciaService"></param>
        /// <param name="administradoresService"></param>
        /// <param name="plantillaService"></param>
        public WhatsAppBusiness (IInfoWhatsAppService infoWhatsAppService, IAgenciaService agenciaService, IAdministradoresService administradoresService, IPlantillaService plantillaService)
        {
            _infoWhatsAppService = infoWhatsAppService;
            _genciaService = agenciaService;
            _administradoresService = administradoresService;
            _plantillaService = plantillaService;
        }

        /// <summary>
        /// Crea un registro de WhatsApp de una agencia comprobando si es agencia, administrador y puede usar SMS
        /// </summary>
        /// <param name="model"></param>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async Task<int> CreateInfoWhatsApp (CreateInfoWhatsAppRequest model, string agenciaNombre, string agenciaToken, string adminEmail, string adminToken)
        {
            return await _infoWhatsAppService.CreateInfoWhatsApp(model, await GetIdAgencia(adminEmail, adminToken, agenciaNombre, agenciaToken));
        }
        /// <summary>
        /// Elimina un registro de WhatsApp de una agencia comprobando si es agencia, administrador y puede usar WhatsApp
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
        public async Task<int> DeleteInfoWhatsApp (int id, string agenciaNombre, string agenciaToken, string adminEmail, string adminToken)
        {
            return await _infoWhatsAppService.DeleteInfoWhatsApp(id, await GetIdAgencia(adminEmail, adminToken, agenciaNombre, agenciaToken));
        }
        /// <summary>
        /// Obtiene todos los registros WhatsApp de una agencia comprobando si es agencia, administrador y puede usar WhatsApp
        /// </summary>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async  Task<IEnumerable<InfoWhatsApp>> GetAllInfoWhatsApp ( string agenciaNombre, string agenciaToken, string adminEmail, string adminToken)
        {
            return await _infoWhatsAppService.GetAllInfoWhatsApp(await GetIdAgencia(adminEmail, adminToken, agenciaNombre, agenciaToken));
        }
        /// <summary>
        /// Actualiza un registro WhatsApp de una agencia comprobando si es agencia, administrador y puede usar WhatsApp
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async Task<int> UpdateInfoWhatsApp (int id, UpdateInfoWhatsAppRequest model, string agenciaNombre, string agenciaToken, string adminEmail, string adminToken)
        {
            return await _infoWhatsAppService.UpdateInfoWhatsApp(id, model, await GetIdAgencia(adminEmail, adminToken, agenciaNombre, agenciaToken));
        }
        /// <summary>
        /// Devuelve un registro WhatsApp de una agencia comprobando si es agencia, administrador y puede usar WhatsApp
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <returns>Un registro de WhatsApp</returns>
        public async Task<InfoWhatsApp> GetInfoWhatsAppById (int id, string agenciaNombre, string agenciaToken, string adminEmail, string adminToken)
        {
           return await _infoWhatsAppService.GetInfoWhatsAppById(id,await GetIdAgencia(adminEmail, adminToken, agenciaNombre, agenciaToken));
        }
        /// <summary>
        /// Envía un WhatsApp obteniendo los datos para el envío, rellenando la plantilla, comprobando si es agencia, administrador y puede usar WhatsApp
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Enviar (EnviarWhatsApp model)
        {
            int agenciaId = await GetIdAgencia(model.adminEmail, model.adminToken, model.agenciaNombre, model.agenciaToken);
            InfoWhatsApp infoWhatsApp = await _infoWhatsAppService.GetInfoWhatsAppByName(model.nombre, agenciaId);
            string plantilla = _infoWhatsAppService.GetJsonWhatsApp();
            plantilla = plantilla.Replace("${template}", model.plantilla);
            plantilla = plantilla.Replace("${idioma}", infoWhatsApp.idioma);
            string url = _infoWhatsAppService.GetURLWhatsApp();
            foreach (var numero in model.numeros)
            {
                plantilla = plantilla.Replace("${numero}", model.mensaje);
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        // Configura las cabeceras HTTP
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + infoWhatsApp.token);
                        client.DefaultRequestHeaders.Add("Content-Type", "application/json");

                        // Configura el contenido JSON en el cuerpo de la solicitud
                        var content = new StringContent(plantilla, Encoding.UTF8, "application/json");

                        // Envía la solicitud POST
                        HttpResponseMessage response = await client.PostAsync(url, content);

                        // Lee la respuesta
                        string responseContent = await response.Content.ReadAsStringAsync();

                        // Imprime la respuesta en la consola
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    return 1;
                }
            }
            return 0;
        }
        /// <summary>
        /// Comprueba que sea agencia, administrador y que pueda usar WhatsApp
        /// </summary>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <returns>Identificador de la agencia</returns>
        /// <exception cref="Exception">No existe la agencia</exception>
        /// <exception cref="Exception">La agencia no puede usar WhatsApp</exception>
        private async Task<int> GetIdAgencia (string adminEmail, string adminToken, string agenciaNombre, string agenciaToken)
        {
            int agenciaId = 0;
            bool esAdmin = await _administradoresService.IsAdministrador(adminEmail, adminToken);
            if (esAdmin)
            {
                Agencia agencia = await _genciaService.GetAgenciaByName(agenciaNombre);
                agenciaId = agencia.id;
                if (!agencia.puedeWhatsApp)
                {
                    throw new Exception("La agencia no puede usar WhatsApp");
                }
            }
            else
            {
                Agencia agencia = await _genciaService.GetAgenciaByNameAndToken(agenciaNombre, agenciaToken);
                agenciaId = agencia.id;
                if (!agencia.puedeWhatsApp)
                {
                    throw new Exception("La agencia no puede usar WhatsApp");
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


using Mensajeria_Linux.Business.Interfaces;
using Mensajeria_Linux.Business.Models;
using Mensajeria_Linux.Controllers.Email.Models;
using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.EntityFramework.Models.InfoEmail;
using Mensajeria_Linux.Services.Interfaces;
using System.Text;

namespace Mensajeria_Linux.Business
{
    /// <summary>
    /// Capa business de Email
    /// </summary>
    public class EmailBusiness :IEmailBusiness
    {
        private IInfoEmailService _infoEmailService;
        private IAgenciaService _genciaService;
        private IAdministradoresService _administradoresService;
        private IPlantillaService _plantillaService;
        /// <summary>
        /// Constructor de EmailBusiness
        /// </summary>
        /// <param name="infoEmailService"></param>
        /// <param name="agenciaService"></param>
        /// <param name="administradoresService"></param>
        /// <param name="plantillaService"></param>
        public  EmailBusiness (IInfoEmailService infoEmailService, IAgenciaService agenciaService,
            IAdministradoresService administradoresService, IPlantillaService plantillaService)        
        {
            _infoEmailService = infoEmailService;            
            _genciaService = agenciaService;
            _administradoresService = administradoresService;
            _plantillaService = plantillaService;   
        }
        /// <summary>
        /// Crea datos de un email comprobando que sea agencia o adminstrador y que esta pueda usar emails
        /// </summary>
        /// <param name="model"></param>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async Task<int> CreateInfoEmail (CreateInfoEmailRequest model, string adminEmail, string adminToken)
        {

            int agenciaId = await GetIdAgencia(adminEmail, adminToken, model.nombreAgencia, model.tokenAgencia);
            if (agenciaId != 0) {
                return await _infoEmailService.CreateInfoEmail(model, agenciaId);
            }
            return 0;
        }
        /// <summary>
        /// Obtiene todos los registros de Email de una agencia comprobando que sea agencia, administrador y pueda usar Emails
        /// </summary>
        /// <param name="agenciaName"></param>
        /// <param name="token"></param>
        /// <param name="emailAdmin"></param>
        /// <param name="tokenAdmin"></param>
        /// <returns>Lista de registro de Emails</returns>
        public async Task<IEnumerable<InfoEmail>> GetAllInfoEmail (string agenciaName, string? token, string? emailAdmin, string? tokenAdmin)
        {
            int agenciaId = await GetIdAgencia(emailAdmin, tokenAdmin, agenciaName, token);
            if (agenciaId != 0)
            {
                return await _infoEmailService.GetAllInfoEmailByAgencia(agenciaId);
            }
            return new List<InfoEmail>();
        }
        /// <summary>
        /// Obtiene el registro de un email de una agencia comprobando que sea agencia, administrador y pueda usar Emails
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agenciaName"></param>
        /// <param name="token"></param>
        /// <param name="emailAdmin"></param>
        /// <param name="tokenAdmin"></param>
        /// <returns>Registro de un Email en concreto</returns>
        public async Task<InfoEmail> GetInfoEmailById (int id, string agenciaName, string? token, string? emailAdmin, string? tokenAdmin)
        {
            return await _infoEmailService.GetInfoEmailById(id, await GetIdAgencia(emailAdmin, tokenAdmin, agenciaName,token));
        }
        /// <summary>
        /// Actualiza el registro de un email de una agencia comprobando que sea agencia, administrador y pueda usar Emails
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="emailAdmin"></param>
        /// <param name="tokeAdmin"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async Task<int> UpdateInfoEmail (int id, UpdateInfoEmailRequest model, string emailAdmin, string tokeAdmin)
        {
            
            return await _infoEmailService.UpdateEmailTeams(id, model, await GetIdAgencia(emailAdmin, tokeAdmin, model.nombreAgencia, model.tokenAgencia));
            
        }
        /// <summary>
        /// Elimina el registro de Email de una agencia comprobando que sea agencia, administrador y pueda usar Emails
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agenciaName"></param>
        /// <param name="token"></param>
        /// <param name="emailAdmin"></param>
        /// <param name="tokenAdmin"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async Task<int> DeleteInfoEmail (int id, string agenciaName, string? token, string? emailAdmin, string? tokenAdmin)
        {
            
           return await _infoEmailService.DeleteInfoEmail(id, await GetIdAgencia(emailAdmin, tokenAdmin, agenciaName, token));
            
        }
        /// <summary>
        /// Envía un email rellenando los datos necesarios para esto, rellenando la plantilla, comprobando si es agencia o admin y verificcando que la agencia puede enviar emails
        /// </summary>
        /// <param name="datos"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async Task<int> EnviarProcesoAutorizacion(EnviarEmail datos)
        {
            int agenciaId = await GetIdAgencia(datos.emailAdmin, datos.tokenAdmin, datos.nombreAgencia, datos.tokenAgencia);
            if (agenciaId != 0)
            {
                InfoEmail infoEmail = await _infoEmailService.GetInfoEmailByAgenciaIdAndTipo(agenciaId,datos.emailOrigen);
                if (infoEmail == null)  return 0; 
                string plantilla = await _plantillaService.GetContenidoPlantillaByNameAndExtensionAndAgenciaId(datos.plantilla, "HTML",agenciaId);
                if(plantilla == null) return 0;
                string plantillaRellena = await RellenarPlantilla( plantilla,  datos.autorizacionDatos,agenciaId);
                int devuelto =  _infoEmailService.EnviarEmailConPlantillaADestino(plantillaRellena, infoEmail, datos.emailsDestino, datos.asunto, datos.ficheros);
                return devuelto;
            }
            return 0;
        }
        /// <summary>
        /// Función que rellena la plantilla de autorización de datos en HTML
        /// </summary>
        /// <param name="plantilla"></param>
        /// <param name="datos"></param>
        /// <param name="agenciaID"></param>
        /// <returns>Plantilla rellena en formato HTML</returns>
        private async Task<string> RellenarPlantilla(string plantilla, AutorizacionDatos datos, int agenciaID)
        {
            plantilla = plantilla.Replace("{identificador}", datos.identificador);
            plantilla = plantilla.Replace("{viajeReserva}", PintarNombreCompleto(datos.viajeroReserva));
            plantilla = plantilla.Replace("{solicitante}", PintarNombreCompleto(datos.solicitante));
            plantilla = plantilla.Replace("{trayectos}", PintarTrayecto(datos.trayectos));
            plantilla = plantilla.Replace("{fechaLimite}",datos.fechalimite.ToString());
            plantilla = plantilla.Replace("{importe}", datos.importe.ToString());
            plantilla = plantilla.Replace("{observaciones}", datos.observaciones);
            plantilla = plantilla.Replace("{datosRedireccionamiento}", datos.datosRedireccionamiento);
            string plantillaFooter =  await _plantillaService.GetContenidoPlantillaByNameAndExtensionAndAgenciaId("Footer", "HTML",agenciaID);
            string plantillaFinal = plantilla + plantillaFooter;
            return plantillaFinal;

        }

        /// <summary>
        /// Método que genera una cadena de nombre
        /// </summary>
        /// <param name="nombres"></param>
        /// <returns>Nombre formateado</returns>
        private string PintarNombreCompleto (List<NombreCompleto> nombres)
        {
            StringBuilder sb = new StringBuilder();
            foreach (NombreCompleto nombre in nombres)
            {
                sb.Append($"<li><span>Nombre Completo:</span><span>{nombre.nombre}</span><span>{nombre.apellido1}</span><span>{nombre.apellido2}</span></li>");
            }
            return sb.ToString();
        }
        /// <summary>
        /// Método que genera una cadena con los trayectos
        /// </summary>
        /// <param name="trayectos"></param>
        /// <returns>Trayecto formateado</returns>
        private string PintarTrayecto (List<Trayecto> trayectos)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Trayecto trayecto in trayectos)
            {

                sb.Append($"<ul>" +
                    $"<li>Fecha de Salida: <span>{trayecto.fechaSalida}</span></li>" +
                    $"<li>Origen: <span>{trayecto.origen}</span></li>" +
                    $"<li>Fecha de Llegada: <span>{trayecto.fechaLlegada}</span></li>" +
                    $"<li>Destino: <span>{trayecto.destino}</span></li>" +
                    $"</ul>");
            }
            return sb.ToString();
        }
        /// <summary>
        /// Comprueba que sea agencia, administrador y que pueda usar Emails
        /// </summary>
        /// <param name="adminEmail"></param>
        /// <param name="adminToken"></param>
        /// <param name="agenciaNombre"></param>
        /// <param name="agenciaToken"></param>
        /// <returns>Identificador de la agencia</returns>
        /// <exception cref="Exception">No existe la agencia</exception>
        /// <exception cref="Exception">La agencia no puede usar Emails</exception>
        private async Task<int> GetIdAgencia (string adminEmail, string adminToken, string agenciaNombre, string agenciaToken)
        {
            int agenciaId = 0;
            bool esAdmin = await _administradoresService.IsAdministrador(adminEmail, adminToken);
            if (esAdmin)
            {
                Agencia agencia = await _genciaService.GetAgenciaByName(agenciaNombre);
                agenciaId = agencia.id;
                if (!agencia.puedeEmail)
                {
                    throw new Exception("La agencia no puede usar Emails");
                }
            }
            else
            {
                Agencia agencia = await _genciaService.GetAgenciaByNameAndToken(agenciaNombre, agenciaToken);
                agenciaId = agencia.id;
                if (!agencia.puedeEmail)
                {
                    throw new Exception("La agencia no puede usar Emails");
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

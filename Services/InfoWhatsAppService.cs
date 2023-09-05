using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mensajeria_Linux.EntityFramework.Data;
using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.EntityFramework.Helpers;
using Mensajeria_Linux.EntityFramework.Models.InfoWhatsApp;
using Mensajeria_Linux.Services.Interfaces;


namespace Mensajeria_Linux.Services
{
    /// <summary>
    /// Capa servicio de WhatsApp
    /// </summary>
    public class InfoWhatsAppService : IInfoWhatsAppService
    {
        private NotificationContext _dbCntext;
        private readonly IMapper _mapper;
        private readonly string jsonBody = @"{
            ""messaging_product"": ""whatsapp"",
            ""to"": ""${numero}"",
            ""type"": ""template"",
            ""template"": {
                ""name"": ""${template}"",
                ""language"": {
                    ""code"": ""${idioma}""
                }
            }
        }";
        private readonly string url = "https://graph.facebook.com/v17.0//messages";
        /// <summary>
        /// Constructor de la capa servicio de WhatsApp
        /// </summary>
        /// <param name="dbCntext"></param>
        /// <param name="mapper"></param>
        public InfoWhatsAppService (NotificationContext dbCntext, IMapper mapper)
        {
            _dbCntext = dbCntext;
            _mapper = mapper;
        }
        /// <summary>
        /// Obtención de Json para enviar a WhatsApp
        /// </summary>
        /// <returns>String con la plantilla de envío de mensajes de WhatsApp</returns>
        public string GetJsonWhatsApp ( )
        {
            return jsonBody;
        }
        /// <summary>
        /// Obtención de URL destino
        /// </summary>
        /// <returns>String con la url que se va a enviar el WhatsApp</returns>
        public string GetURLWhatsApp ( )
        {
            return url;
        }
        /// <summary>
        /// Creación de un registro WhatsApp
        /// </summary>
        /// <param name="model"></param>
        /// <param name="agenciaId"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async Task<int> CreateInfoWhatsApp (CreateInfoWhatsAppRequest model, int agenciaId)
        {
            if (await _dbCntext.infoWhatsApp.AnyAsync(x => x.token == model.token))
            {
                return 0;
            }
            //Hace un mapa del modelo InfoTeams
            InfoWhatsApp infoWhatsApp = _mapper.Map<InfoWhatsApp>(model);
            infoWhatsApp.agenciaId = agenciaId;
            //Guardar InfoTeams
            _dbCntext.infoWhatsApp.Add(infoWhatsApp);
            await _dbCntext.SaveChangesAsync().ConfigureAwait(true);
            if (infoWhatsApp != null)
            {
                return infoWhatsApp.id;
            }

            return 0;
        }
        /// <summary>
        /// Eliminación de un registro WhatsApp
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agenciaId"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async Task<int> DeleteInfoWhatsApp (int id, int agenciaId)
        {
            InfoWhatsApp? infoWhatsApp = await _getInfoWhatsAppsByIdAndAgenciaId(id, agenciaId);
            if (infoWhatsApp != null)
            {
                _dbCntext?.infoWhatsApp.Remove(infoWhatsApp);
                await _dbCntext.SaveChangesAsync().ConfigureAwait(true);
                return 0;
            }
            else
            {
                throw new RepositoryExceptions($"No existe InfoTeams de usuario con id {id} ya existe.");
            }
        }
        /// <summary>
        /// Obtener todos los registros WhatsApp de una agencia
        /// </summary>
        /// <param name="agenciaId"></param>
        /// <returns>Lista de registros WhatsApp</returns>
        public async Task<IEnumerable<InfoWhatsApp>> GetAllInfoWhatsApp (int agenciaId)
        {
            //Devuelve todos los InfoTeams
            return await _dbCntext.infoWhatsApp.Where(x=> x.agenciaId == agenciaId).ToArrayAsync().ConfigureAwait(true);
        }
        /// <summary>
        /// Actualización de un registro WhatsApp
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="agenciaId"></param>
        /// <returns></returns>
        public async Task<int> UpdateInfoWhatsApp (int id, UpdateInfoWhatsAppRequest model, int agenciaId)
        {
            InfoWhatsApp? infoWhatsApp = await _getInfoWhatsAppsByIdAndAgenciaId(id, agenciaId);
            // Validation
            if (model.token != infoWhatsApp.token && await _dbCntext.infoWhatsApp.AnyAsync(x => x.token == model.token))
            {
                throw new RepositoryExceptions($"El token {model.token} ya existe.");
            }               

            _mapper.Map(model, infoWhatsApp);
            _dbCntext.infoWhatsApp.Update(infoWhatsApp);
            return 0;
        }
        /// <summary>
        /// Obtención de un registro de WhatsApp por id y agencia
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agenciaId"></param>
        /// <returns>Registro WhatsApp</returns>
        public async Task<InfoWhatsApp> GetInfoWhatsAppById (int id, int agenciaId)
        {
            
            return await _getInfoWhatsAppsByIdAndAgenciaId( id,  agenciaId);
        }
        /// <summary>
        /// Obtención de un registro de WhatsApp por nombre y agencia
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="agenciaId"></param>
        /// <returns>Un registro de InfoWhtasApp</returns>
        /// <exception cref="KeyNotFoundException">InfoWhatsApp no se ha encontrado en la base de datos</exception>
        public async Task<InfoWhatsApp> GetInfoWhatsAppByName (string nombre, int agenciaId)
        {
            InfoWhatsApp? infoWhatsApp = await _dbCntext.infoWhatsApp
                .AsNoTracking()
                .Where(x => x.nombre == nombre && x.agenciaId==agenciaId)
                .FirstOrDefaultAsync().ConfigureAwait(true);
            if (infoWhatsApp == null)
            {
                throw new KeyNotFoundException("InfoWhatsApp no se ha encontrado en la base de datos");
            }
            return infoWhatsApp;
        }
        /// <summary>
        /// btención de un registro de WhatsApp por id y agencia
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agenciaId"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        private async Task<InfoWhatsApp> _getInfoWhatsAppsByIdAndAgenciaId (int id, int agenciaId)
        {
            InfoWhatsApp? infoWhatsApp = await _dbCntext.infoWhatsApp
                .AsNoTracking()
                .Where(x => x.id == id && x.agenciaId==agenciaId)
                .FirstOrDefaultAsync().ConfigureAwait(true);
            if (infoWhatsApp == null)
            {
                throw new KeyNotFoundException("InfoTeams no se ha encontrado en la base de datos");
            }
            return infoWhatsApp;
        }
        
    }
}

using AutoMapper;
using Mensajeria_Linux.EntityFramework.Data;
using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.EntityFramework.Helpers;
using Mensajeria_Linux.EntityFramework.Models.InfoSMS;
using Mensajeria_Linux.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mensajeria_Linux.Services
{
    /// <summary>
    /// Capa servicio de SMS
    /// </summary>
    public class SMSService : ISMSService
    {
        private NotificationContext _dbCntext;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor de la capa servicio de SMS
        /// </summary>
        /// <param name="dbCntext"></param>
        /// <param name="mapper"></param>
        public SMSService (NotificationContext dbCntext, IMapper mapper)
        {
            _dbCntext = dbCntext;
            _mapper = mapper;
        }

        /// <summary>
        /// Creación de un SMS
        /// </summary>
        /// <param name="model"></param>
        /// <param name="agenciaId"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async Task<int> CreateInfoSMS (CreateInfoSMSRequest model, int agenciaId)
        {
            if (await _dbCntext.infoSMS.AnyAsync(x => x.nombre == model.nombre))
            {
                return 0;
            }
            InfoSMS infoSMS = _mapper.Map<InfoSMS>(model);
            infoSMS.agenciaId = agenciaId;
            infoSMS.created = DateTime.Now;

            _dbCntext.infoSMS.Add(infoSMS);
            try
            {
                await _dbCntext.SaveChangesAsync().ConfigureAwait(true);
                return infoSMS.id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        /// <summary>
        /// Eeliminación de un SMS
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agenciaId"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async Task<int> DeleteSMS (int id, int agenciaId)
        {
            InfoSMS? infoSMS = await _getInfoSMSByIdAndAgenciaId(id, agenciaId);
            if (infoSMS != null)
            {
                _dbCntext?.infoSMS.Remove(infoSMS);
                return await _dbCntext.SaveChangesAsync().ConfigureAwait(true);

            }
            else
            {
                throw new RepositoryExceptions($"No existe infoSMS  con id {id}");
            }
        }
        /// <summary>
        /// Obtención de un registro SMS por id y agencia
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agenciaId"></param>
        /// <returns>Registro de InfoSMS</returns>
        public async Task<InfoSMS> GetInfoSMSlById (int id, int agenciaId)
        {           
            return await _getInfoSMSByIdAndAgenciaId(id, agenciaId);
        }
        /// <summary>
        /// Obtención de un registro SMS por nombre y agencia
        /// </summary>
        /// <param name="name"></param>
        /// <param name="agenciaId"></param>
        /// <returns>Registro de InfoSMS</returns>
        public async Task<InfoSMS> GetInfoSMSlByNameAndAgenciaId (string name, int agenciaId)
        {
            return await _getInfoSMSByNameAndAgenciaId(name, agenciaId);
        }
        /// <summary>
        /// Obtención de todos los registro de una agencia
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Lista de registros de InfoSMS</returns>
        public Task<IEnumerable<InfoSMS>> GetAllInfoSMSByAgencia (int id)
        {
            return _getAllInfoSMSByAgencia(id);
        }
        /// <summary>
        /// Aactualización de un SMS
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="agenciaId"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async Task<int> UpdateSMSTeams (int id, UpdateInfoSMSRequest model, int agenciaId)
        {
            InfoSMS? infoSMS = await _getInfoSMSByIdAndAgenciaId(id, agenciaId);
            // Validation
            if (model.nombre != infoSMS.nombre && await _dbCntext.infoSMS.AnyAsync(x => x.nombre == model.nombre))
                throw new RepositoryExceptions($"{model.nombre} ya existe.");

            _mapper.Map(model, infoSMS);
            _dbCntext.infoSMS.Update(infoSMS);
            return await _dbCntext.SaveChangesAsync().ConfigureAwait(true);
        }
        /// <summary>
        /// Obtención de todos los registro de una agencia
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Lista de registros de InfoSMS</returns>
        public async Task<IEnumerable<InfoSMS>> _getAllInfoSMSByAgencia (int id)
        {
            IEnumerable<InfoSMS>? infoSMS = await _dbCntext.infoSMS
                     .AsNoTracking()
                     .Where(x => x.agenciaId == id)
                     .ToArrayAsync().ConfigureAwait(true);

            if (infoSMS == null)
            {
                throw new KeyNotFoundException("InfoSMS  not found");
            }
            else
            {
                return infoSMS;
            }


        }
        /// <summary>
        /// Obtención de un registro SMS por id y agencia
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agenciaId"></param>
        /// <returns>Registro de InfoSMS</returns>
        private async Task<InfoSMS> _getInfoSMSByIdAndAgenciaId (int id, int agenciaId)
        {
            InfoSMS? infoSMS = await _dbCntext.infoSMS
                .AsNoTracking()
                .Where(x => x.id == id && x.agenciaId == agenciaId)
                .FirstOrDefaultAsync().ConfigureAwait(true);
            if (infoSMS == null)
            {
                throw new KeyNotFoundException("InfoSMS no se ha encontrado en la base de datos");
            }
            return infoSMS;
        }
        /// <summary>
        /// Obtención de un registro SMS por nombre y agencia
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="agenciaId"></param>
        /// <returns>Registro de InfoSMS</returns>
        private async Task<InfoSMS> _getInfoSMSByNameAndAgenciaId (string nombre, int agenciaId)
        {
            InfoSMS? infoSMS = await _dbCntext.infoSMS
                .AsNoTracking()
                .Where(x => x.nombre == nombre && x.agenciaId == agenciaId)
                .FirstOrDefaultAsync().ConfigureAwait(true);
            if (infoSMS == null)
            {
                throw new KeyNotFoundException("InfoSMS no se ha encontrado en la base de datos");
            }
            return infoSMS;
        }
    }
}

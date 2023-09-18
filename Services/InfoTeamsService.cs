using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mensajeria_Linux.EntityFramework.Data;
using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.Services.Interfaces;
using Mensajeria_Linux.EntityFramework.Models.InfoTeam;
using Mensajeria_Linux.EntityFramework.Helpers;

namespace Mensajeria_Linux.Services
{
    /// <summary>
    /// Capa de servio de Teams
    /// </summary>
    public class InfoTeamsService : IInfoTeamService
    {
        private NotificationContext _dbCntext;
        private readonly IMapper _mapper;
        /// <summary>
        /// Constructor de la capa servicio de Teams
        /// </summary>
        /// <param name="dbCntext"></param>
        /// <param name="mapper"></param>
        public InfoTeamsService (NotificationContext dbCntext, IMapper mapper)
        {
            _dbCntext = dbCntext;
            _mapper = mapper;
        }
        /// <summary>
        /// Creación de un registro Teams, comprueba que no exista un WebHook igual
        /// </summary>
        /// <param name="model"></param>
        /// <param name="agenciaId"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async Task<int> CreateInfoTeams (CreateInfoTeamsRequest model, int agenciaId)
        {
            
            if (await _dbCntext.infoTeams.AnyAsync(x => x.webHook == model.webHook || x.nombre== model.nombre))
            {
                return 0;
            }            
            //Hace un mapa del modelo InfoTeams
            InfoTeams infoTeams = _mapper.Map<InfoTeams>(model);
            infoTeams.agenciaId= agenciaId;
            //Guardar InfoTeams
            _dbCntext.infoTeams.Add(infoTeams);
             await _dbCntext.SaveChangesAsync().ConfigureAwait(true);
            if(infoTeams != null)
            {
                return infoTeams.id;
            }
            return 0;
        }
        /// <summary>
        /// Eliminación de un registro Teams
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agenciaId"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async Task<int> DeleteInfoTeams (int id, int agenciaId)
        {
            InfoTeams? infoTeams = await _getInfoTeamsByIdAndAgenciaId(id,agenciaId);
            if (infoTeams != null)
            {
                _dbCntext?.infoTeams.Remove(infoTeams);
                await _dbCntext.SaveChangesAsync().ConfigureAwait(true);
                return 0;
            }
            else
            {
                throw new RepositoryExceptions($"No existe InfoTeams de usuario con id {id}");
            }
        }
        /// <summary>
        /// Devuelve todos los registros de teams de una agencia
        /// </summary>
        /// <param name="agenciaId"></param>
        /// <returns>Lista de InfoTeams</returns>
        public async Task<IEnumerable<InfoTeams>> GetAllInfoTeamsByAgenciaId (int agenciaId)
        {
            return await _dbCntext.infoTeams.Where(x=>x.agenciaId==agenciaId).ToArrayAsync().ConfigureAwait(true);
        }
        /// <summary>
        /// Actualización de un registro Teams
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="agenciaId"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async Task<int> UpdateInfoTeams (int id, UpdateInfoTeamsRequest model, int agenciaId)
        {
            InfoTeams? infoTeams = await _getInfoTeamsByIdAndAgenciaId(id, agenciaId );
            // Validation
            if (model.webHook != infoTeams.webHook && await _dbCntext.infoTeams.AnyAsync(x => x.webHook == model.webHook))
                throw new RepositoryExceptions($" {model.webHook} ya existe.");

            _mapper.Map(model, infoTeams);
            infoTeams.agenciaId = agenciaId;
            _dbCntext.infoTeams.Update(infoTeams);
            await _dbCntext.SaveChangesAsync().ConfigureAwait(true);
            return 0;
        }
        /// <summary>
        /// Obtener un registro de teams por nombre y agencia
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="agenciaId"></param>
        /// <returns>Un registro de Teams</returns>
        public async Task<InfoTeams> GetInfoTeamsByNombreAdnAgenciaId (string nombre, int agenciaId)
        {
            return await  _getInfoTeamsByNombreAdnAgenciaId(nombre, agenciaId);
        }
        /// <summary>
        /// Obtener un registro de teams por nombre y agencia
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="agenciaId"></param>
        /// <returns>Un registro de Teams</returns>
        private async Task<InfoTeams> _getInfoTeamsByNombreAdnAgenciaId (string nombre, int agenciaId)
        {
            InfoTeams? infoTeams = await _dbCntext.infoTeams
                .AsNoTracking()
                .Where(x => x.nombre == nombre && x.agenciaId== agenciaId)
                .FirstOrDefaultAsync().ConfigureAwait(true);
            if (infoTeams == null)
            {
                throw new KeyNotFoundException("InfoTeams no se ha encontrado en la base de datos");
            }
            return infoTeams;
        }
        /// <summary>
        /// Obtener un registro de teams por id y agencia
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agenciaId"></param>
        /// <returns>Un registro de Teams</returns>
        private async Task<InfoTeams> _getInfoTeamsByIdAndAgenciaId (int id, int agenciaId)
        {
            InfoTeams? infoTeams = await _dbCntext.infoTeams
                .AsNoTracking()
                .Where(x=> x.id == id && x.agenciaId ==agenciaId)
                .FirstOrDefaultAsync().ConfigureAwait(true);
            if(infoTeams == null)
            {
                throw new KeyNotFoundException("InfoTeams no se ha encontrado en la base de datos");
            }
            return infoTeams;
        }

    }
}
       
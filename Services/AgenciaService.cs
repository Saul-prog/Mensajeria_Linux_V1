using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mensajeria_Linux.EntityFramework.Data;
using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.EntityFramework.Helpers;
using Mensajeria_Linux.EntityFramework.Models.Agencias;
using Mensajeria_Linux.Services.Interfaces;
using System.Xml.Linq;

namespace Mensajeria_Linux.Services
{
    /// <summary>
    /// Capa servicio de Agencia
    /// </summary>
    public class AgenciaService : IAgenciaService
    {

        private NotificationContext _dbContext;
        private readonly IMapper _mapper;
        /// <summary>
        /// Constructor de la capa servicio de agencia
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public AgenciaService (NotificationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        /// <summary>
        /// Creación de una Agencia
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async Task<int> CreateAgencia (CreateAgenciaRequest model)
        {
            if (await _dbContext.Agencias.AnyAsync(x => x.nombreAgencia == model.nombreAgencia))
            {
                return 0;
            }
            Agencia agencia = _mapper.Map<Agencia>(model);
            _dbContext.Agencias.Add(agencia);
           if( await _dbContext.SaveChangesAsync().ConfigureAwait(true) != 1 )
            {
                return agencia.id;
            }

            return 1;
        }
        /// <summary>
        /// Eliminación de una Agencia
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async Task<int> DeleteAgencia ( string name, int id)
        {
            Agencia? agencia = await _getagenciaByNameAndAdminId(name, id);
            _dbContext.Agencias.Remove(agencia);
            return await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Actualización de una Agencia
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async Task<int> UpdateAgencia (UpdateAgenciaRequest model)
        {
            Agencia agencia = await _getagenciaByNameAndAdminId(model.nombreAgencia, model.AdminsitradorId);
            _mapper.Map(model, agencia);
            _dbContext.Agencias.Update(agencia);
            return await _dbContext.SaveChangesAsync();
        }

        private async Task<Agencia> _getagenciaByName (string name)
        {
            Agencia? agencia = await _dbContext.Agencias
                .AsNoTracking()
                .Where(x => x.nombreAgencia == name).
                FirstOrDefaultAsync().ConfigureAwait(true);
            if (agencia == null)
            {
                throw new RepositoryExceptions("Agencia no encontrada");
            }
            return agencia;
        }
        /// <summary>
        /// Obtención de una agencia por el nombre
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Agencia</returns>
        public async Task<Agencia> GetAgenciaByName (string name)
        {
            return await _getagenciaByName(name);
        }
        /// <summary>
        /// Obtención de una agencia por nombre e id de administrador
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Agencia</returns>
        public async Task<IEnumerable<Agencia>> GetAllAgenciaByAdministradorId (int id)
        {
            return await _dbContext.Agencias
                .Where(x => x.AdminsitradorId==id)
                .ToArrayAsync()
                .ConfigureAwait(true);
        }
        /// <summary>
        /// Obtención de una Agencia mediante Nombre y token
        /// </summary>
        /// <param name="name"></param>
        /// <param name="token"></param>
        /// <returns>Agencia</returns>
        public async Task<Agencia> GetAgenciaByNameAndToken (string name,string token)
        {
            return await _getagenciaByNameAndToken(name,token);
        }
        /// <summary>
        /// Obtención de un identificador de agencia mediante nombre y token
        /// </summary>
        /// <param name="name"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<int> GetAgenciaIdByNameAndToken(string name,string token)
        {
            return await _getAgenciaIdByNameAndToken(name, token);
        }

        /// <summary>
        /// IObtención de una agencia por nombre e id de administrador
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns>Agencia</returns>
        public async Task<Agencia> GetAgenciaByNameAndAdministradorId (string name, int id)
        {
            return await _getagenciaByNameAndAdminId(name, id);
        }

        /// <summary>
        /// Obtención de un identificador de agencia mediante nombre y token
        /// </summary>
        /// <param name="name"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<Agencia> _getagenciaByNameAndToken (string name, string token)
        {
            Agencia? agencia = await _dbContext.Agencias
                .AsNoTracking()
                .Where(x => x.nombreAgencia == name && x.token == token)
                .FirstOrDefaultAsync().ConfigureAwait(true);
            if (agencia == null)
            {
                throw new RepositoryExceptions("Agencia no encontrada");
            }
            return agencia;
        }
        /// <summary>
        /// IObtención de una agencia por nombre e id de administrador
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns>Agencia</returns>
        private async Task<Agencia> _getagenciaByNameAndAdminId (string name, int id)
        {
            Agencia? agencia = await _dbContext.Agencias
                .AsNoTracking()
                .Where(x => x.nombreAgencia == name && x.AdminsitradorId == id)
                .FirstOrDefaultAsync().ConfigureAwait(true);
            if (agencia == null)
            {
                throw new RepositoryExceptions("Agencia no encontrada");
            }
            return agencia;
        }
        /// <summary>
        /// Obtención de un identificador de agencia mediante nombre y token
        /// </summary>
        /// <param name="name"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<int> _getAgenciaIdByNameAndToken(string name,string token)
        {
            int agenciaId = await _dbContext.Agencias
                .AsNoTracking()
                .Where(x => x.nombreAgencia == name && x.token == token)
                .Select(x => x.id)
                .FirstOrDefaultAsync().ConfigureAwait(true);
           return agenciaId;
        }
       
    }
}

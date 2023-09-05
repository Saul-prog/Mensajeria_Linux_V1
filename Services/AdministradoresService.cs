using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mensajeria_Linux.EntityFramework.Data;
using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.Services.Interfaces;

namespace Mensajeria_Linux.Services
{
    public class AdministradoresService : IAdministradoresService
    {
        /// <summary>
        /// Capa servicio de Administradores
        /// </summary>
        private NotificationContext _dbContext;
        private readonly IMapper _mapper;
        /// <summary>
        /// Constructor de la capa sercicio de Administradores
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public AdministradoresService (NotificationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        /// <summary>
        /// Se obtiene el identificador del administrador mediante Email y token
        /// </summary>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <returns>Identificador del administrador</returns>
        public async Task<int> GetAdminidtradorIdByEmailAndToken (string email, string token)
        {
            return await _dbContext.administradores
                     .AsNoTracking()
                     .Where(x => x.email == email && x.token == token)
                     .Select(x => x.id)
                     .FirstOrDefaultAsync()
                     .ConfigureAwait(true);
        }
        /// <summary>
        /// Saber si es administrador
        /// </summary>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <returns>
        ///     true: si es administrador
        ///     false: si no es administrador
        /// </returns>
        public async Task<bool> IsAdministrador(string email, string token)
        {
            bool respuesta = await _dbContext.administradores.AnyAsync(x => x.email == email && x.token == token);
            return respuesta;
        } 
    }
}

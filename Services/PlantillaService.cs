using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mensajeria_Linux.EntityFramework.Data;
using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.EntityFramework.Helpers;
using Mensajeria_Linux.EntityFramework.Models.Plantillas;
using Mensajeria_Linux.Services.Interfaces;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using System.Text;

namespace Mensajeria_Linux.Services
{
    /// <summary>
    /// Capa de servicio de Plantilla
    /// </summary>
    public class PlantillaService : IPlantillaService
    {
        private NotificationContext _dbContext;
        private readonly IMapper _mapper;
        /// <summary>
        /// Constructor de la capa de servicio de Plantilla
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public PlantillaService (NotificationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        /// <summary>
        /// Creación de una plantilla
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async Task<int> CreatePlantilla (CreatePlantillaRequest model, int id)
        {            
            if(await _dbContext.Plantilla.AnyAsync(x => x.nombre == model.nombre))
            {
                return 0;
            }
            Plantillas plantilla = _mapper.Map<Plantillas>(model);
            plantilla.created = DateTime.Now;
            plantilla.agenciaId = id;
            _dbContext.Plantilla.Add(plantilla);
            if (await _dbContext.SaveChangesAsync().ConfigureAwait(true) != 0)
            {
                return plantilla.id;
            }            
            return 0;
        }

        /// <summary>
        /// Eliminación de una plantilla
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async Task<int> DeletePlantillaById (int id)
        {
            Plantillas? plantilla = await _getPlantillaById(id);
            _dbContext.Plantilla.Remove(plantilla);
            return await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Actualización de una plantilla
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async Task<int> UpdatePlantilla (int id,UpdatePlantillaRequest model)
        {
            Plantillas plantilla = await _getPlantillaById(id);
            _mapper.Map(model, plantilla);
            _dbContext.Plantilla.Update(plantilla);
            return await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Obtención de todas las plantillas de una agencia
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Lista de registros de Plantilla</returns>
        public async Task<IEnumerable<Plantillas>> GetAllPlantillasById (int id)
        {
            return await _dbContext.Plantilla.Where(x => x.id == id).ToArrayAsync().ConfigureAwait(true);
        }
        /// <summary>
        /// Obtención de una de las plantillas de una agencia
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Lista de registros de Plantilla</returns>
        private async Task<Plantillas> _getPlantillaById (int id)
        {
            Plantillas? plantilla = await _dbContext.Plantilla
                .AsNoTracking()
                .Where(x => x.id == id).
                FirstAsync().ConfigureAwait(true);
            if (plantilla == null)
            {
                throw new RepositoryExceptions("Usuario no encontrado");
            }
            return plantilla;
        }
        /// <summary>
        /// Interfaz para obtener la plantilla lista para usar mediante el nombre, la extensión y la agencia
        /// </summary>
        /// <param name="name"></param>
        /// <param name="extension"></param>
        /// <param name="agenciaId"></param>
        /// <returns>String de la plantilla</returns>
        public async Task<string> GetContenidoPlantillaByNameAndExtensionAndAgenciaId (string name, string extension, int agenciaId)
        {

            switch (extension)
            {
                case "HTML":
                    return await _getPlantillaHTMLByName(name,agenciaId);
                    
                case "JSON":
                    return await _getPlantillaJSONByName(name,agenciaId);
                    
                case "PLANA":
                    return await _getPlantillaPlanaByName(name, agenciaId);
                    
                default: 
                    throw new RepositoryExceptions("No existe la extensión");
            }

        }
        /// <summary>
        /// Retorna la plantilla HTML
        /// </summary>
        /// <param name="name"></param>
        /// <param name="agenciaId"></param>
        /// <returns>String de la plantilla HTML</returns>
        /// <exception cref="RepositoryExceptions">Plantilla no encontrada</exception>
        private async Task<string> _getPlantillaHTMLByName (string name, int agenciaId)
        {
            string? plantilla = await _dbContext.Plantilla
                .AsNoTracking()
                .Where(x => x.nombre == name && x.agenciaId==agenciaId)
                .Select(x => x.plantillaHtml)
                .FirstAsync().ConfigureAwait(true);
            if (plantilla == null)
            {
                throw new RepositoryExceptions("Plantilla no encontrada");
            }
            return plantilla;
        }
        /// <summary>
        /// Retorna la plantilla JSON 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="agenciaId"></param>
        /// <returns>String de la plantilla JSON</returns>
        /// <exception cref="RepositoryExceptions">Plantilla no encontrada</exception>
        private async Task<string> _getPlantillaJSONByName (string name, int agenciaId)
        {
            byte[]? plantillaJson = await _dbContext.Plantilla
                .AsNoTracking()
                .Where(x => x.nombre == name && x.agenciaId == agenciaId)
                .Select(x => x.plantillaJSON)
                .FirstAsync().ConfigureAwait(true);
            if (plantillaJson == null)
            {
                throw new RepositoryExceptions("Plantilla no encontrada");
            }

            return Encoding.UTF8.GetString(plantillaJson); ;
        }
        /// <summary>
        /// Retorna la plantilla plana
        /// </summary>
        /// <param name="name"></param>
        /// <param name="agenciaId"></param>
        /// <returns>String de la plantilla plana</returns>
        /// <exception cref="RepositoryExceptions">Plantilla no encontrada</exception>
        private async Task<string> _getPlantillaPlanaByName (string name, int agenciaId)
        {
            string? plantilla = await _dbContext.Plantilla
                .AsNoTracking()
                .Where(x => x.nombre == name && x.agenciaId == agenciaId)
                .Select(x => x.plantillaPlana)
                .FirstAsync().ConfigureAwait(true);
            if (plantilla == null)
            {
                throw new RepositoryExceptions("Plantilla no encontrada");
            }
            return plantilla;
        }

    }
}

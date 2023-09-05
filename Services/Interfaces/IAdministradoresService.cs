using Mensajeria_Linux.EntityFramework.Entities;

namespace Mensajeria_Linux.Services.Interfaces
{
    /// <summary>
    /// Interfaz para la capa servicio de Andministradores
    /// </summary>
    public interface IAdministradoresService
    {
        /// <summary>
        /// Interfaz para obtiener el identidficador del administrador mediante Email y token
        /// </summary>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <returns>Identificador del administrador</returns>
        public Task<int> GetAdminidtradorIdByEmailAndToken (string email, string token);
        /// <summary>
        /// Interfaz para saber si es administrador
        /// </summary>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <returns>
        ///     true: si es administrador
        ///     false: si no es administrador
        /// </returns>
        public Task<bool> IsAdministrador (string email, string token);
    }
}

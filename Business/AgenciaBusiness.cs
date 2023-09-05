using AutoMapper;
using Mensajeria_Linux.Business.Interfaces;
using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.EntityFramework.Models.Agencias;
using Mensajeria_Linux.Services.Interfaces;
using System.Security.Cryptography;

namespace Mensajeria_Linux.Business
{
    /// <summary>
    /// Capa business de Agencia
    /// </summary>
    public class AgenciaBusiness : IAgenciaBusiness
    {

        private IAgenciaService _agenciaService;
        private IAdministradoresService _administradoresService;
        /// <summary>
        /// Constructor de la capa business de Agencia
        /// </summary>
        /// <param name="agenciaService"></param>
        /// <param name="administradoresService"></param>
        public AgenciaBusiness (IAgenciaService agenciaService, IAdministradoresService administradoresService)
        {
            _agenciaService = agenciaService;
            _administradoresService = administradoresService;   
        }
        /// <summary>
        /// Crea una agencia comprobando si el administrador es válido, lo asigna a esta como adminstrador y crea un token para esa agencia
        /// </summary>
        /// <param name="model"></param>
        /// <param name="email"></param>
        /// <param name="tokenAdmin"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async Task<int> CreateAgencia (CreateAgenciaRequest model, string email, string tokenAdmin)
        {
            int id;
            try
            {
                id = await ComprobarAdmin(email, tokenAdmin);
            }catch (Exception ex)
            {
                return 0;
            }            
            string token =tokenAdmin+DateTime.Now.ToString();
            model.token = ComputeSHA3Hash(token);
            model.AdminsitradorId = id;
            return await _agenciaService.CreateAgencia (model);
        }
        /// <summary>
        /// Elimina una aggencia comprobando anteriormente que es un administrador
        /// </summary>
        /// <param name="name"></param>
        /// <param name="adminMail"></param>
        /// <param name="adminToken"></param>
        /// <returns></returns>
        public async Task<int> DeleteAgencia (string name, string adminMail, string adminToken)
        {
            int id = await ComprobarAdmin(adminMail, adminToken);
            return await _agenciaService.DeleteAgencia(name,id);
        }
        /// <summary>
        /// Actualiza una agencia comprobando que es un administrador
        /// </summary>
        /// <param name="model"></param>
        /// <param name="adminMail"></param>
        /// <param name="adminToken"></param>
        /// <returns></returns>
        public async Task<int> UpdateAgencia (UpdateAgenciaRequest model,string adminMail, string adminToken)
        {

            int id = await ComprobarAdmin(adminMail, adminToken);
            model.AdminsitradorId=id;
            return  await _agenciaService.UpdateAgencia(model);
        }
        /// <summary>
        /// Obtiene una agencia por nombre comprobando que es un administrador
        /// </summary>
        /// <param name="name"></param>
        /// <param name="adminMail"></param>
        /// <param name="adminToken"></param>
        /// <returns>Datos de una Agencia</returns>
        public async Task<Agencia> GetAgenciaByName(string  name, string adminMail, string adminToken)
        {
            int id = await ComprobarAdmin(adminMail, adminToken);
            return await _agenciaService.GetAgenciaByNameAndAdministradorId(name, id);
        }
        /// <summary>
        /// Obtiene todas las agencias comprobando que es administrador
        /// </summary>
        /// <param name="adminMail"></param>
        /// <param name="adminToken"></param>
        /// <returns>Lista de datos de las agencias</returns>
        public async Task<IEnumerable<Agencia>> GetAllAgencias (string adminMail, string adminToken)
        {
            int id = await ComprobarAdmin(adminMail, adminToken);
            return await _agenciaService.GetAllAgenciaByAdministradorId(id);
        }
        /// <summary>
        /// Genera cadenas Hash mediante SHA512
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Cadena hash de los datos</returns>
        private static string ComputeSHA3Hash (string input)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
                return  BitConverter.ToString(sha512.ComputeHash(inputBytes)).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        /// Comprueba que el administrador que intenta hacer mantenimiento de agencias es válido
        /// </summary>
        /// <param name="adminMail"></param>
        /// <param name="adminToken"></param>
        /// <returns>
        ///     Exito: int > 0
        /// </returns>
        /// <exception cref="Exception">No existe administrador</exception>
        private async Task<int> ComprobarAdmin(string adminMail, string adminToken)
        {
            int id = await _administradoresService.GetAdminidtradorIdByEmailAndToken(adminMail, adminToken);
            if (0 == id)
            {
                throw new Exception("No existe administrador");
            }
            return id;
        }
    }
}

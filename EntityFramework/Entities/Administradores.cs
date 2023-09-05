
namespace Mensajeria_Linux.EntityFramework.Entities
{
    /// <summary>
    /// Entidad de administrador
    /// </summary>
    public class Administradores :  Base
    {
        /// <summary>
        /// Email del administrador
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// Token del administrador
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// Agencias del administrador
        /// </summary>
         public IEnumerable<Agencia>  agencias { get; set; }
}
}

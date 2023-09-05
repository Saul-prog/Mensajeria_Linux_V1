namespace Mensajeria_Linux.EntityFramework.Entities
{
    /// <summary>
    /// Entidad base para la creación del resto de entidades
    /// </summary>
    public class Base
    {
        /// <summary>
        /// Identificador de la entidad
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Fecha de creación de la entidad
        /// </summary>
        public DateTime created { get; set; } 
        /// <summary>
        /// Fecha de actualización de la entidad
        /// </summary>
        public DateTime update { get; set; } = DateTime.Now;
    }
}

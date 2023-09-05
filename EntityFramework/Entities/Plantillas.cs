using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Linux.EntityFramework.Entities
{
    /// <summary>
    /// Entidad de Plantilla
    /// </summary>
    public class Plantillas : Base
    {
        /// <summary>
        /// Nombre de la plantilla
        /// </summary>
        public string nombre { get; set; }
        /// <summary>
        /// Plantilla en HTML
        /// </summary>
        public string plantillaHtml { get; set; }
        /// <summary>
        /// Plantilla en JSON
        /// </summary>
        public byte[] plantillaJSON { get; set; }
        /// <summary>
        /// Plantilla en Texto Plano
        /// </summary>
        public string plantillaPlana { get; set; }
        /// <summary>
        /// Identificador de la agencia a la que pertenece
        /// </summary>
        public int agenciaId { get; set; }
        /// <summary>
        /// Entidad de la agencia a la que pertenece
        /// </summary>
        public Agencia agencia { get; set; }
    }
}

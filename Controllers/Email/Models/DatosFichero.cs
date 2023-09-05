namespace Mensajeria_Linux.Controllers.Email.Models
{
    /// <summary>
    /// Clase para identificar un fichero
    /// </summary>
    public class DatosFichero
    {
        /// <summary>
        /// Nombre del fichero
        /// </summary>
        public string nombreFichero { get; set; }
        /// <summary>
        /// Contenido del fichero en base64
        /// </summary>
        public string contenidoFichero { get; set; }
        /// <summary>
        /// La extensión del fichero
        /// </summary>
        public string extension { get; set; }
    }
}

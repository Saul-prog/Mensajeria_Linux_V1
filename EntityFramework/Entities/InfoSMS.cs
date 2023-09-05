namespace Mensajeria_Linux.EntityFramework.Entities
{
    /// <summary>
    /// Entidad InfoSMS
    /// </summary>
    public class InfoSMS : Base
    {
        /// <summary>
        /// Nombre del registro
        /// </summary>
        public string nombre { get; set; }
        /// <summary>
        /// Clave de acceso de AWS
        /// </summary>
        public string awsAcceskey { get; set; }
        /// <summary>
        /// Clave secreta de acceso de AWS
        /// </summary>
        public string awsSecretKey { get; set; }
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

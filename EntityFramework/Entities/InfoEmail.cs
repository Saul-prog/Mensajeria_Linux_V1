namespace Mensajeria_Linux.EntityFramework.Entities
{
    /// <summary>
    /// Entidad de InfoEmail
    /// </summary>
    public class InfoEmail : Base
    {
        /// <summary>
        /// Host del servicio de correo electronico
        /// </summary>
        public string host { get; set; }
        /// <summary>
        /// Puerto del servicio de correo electrónico
        /// </summary>
        public int port { get; set; }
        /// <summary>
        /// Correo electrónico de origen
        /// </summary>
        public string emailOrigen { get; set; }
        /// <summary>
        /// Clave de acceso del correo electronico de origen
        /// </summary>
        public string emailTokenPassword { get; set; }
        /// <summary>
        /// Nombre del email de origen
        /// </summary>
        public string emailNombre { get; set; }
        /// <summary>
        /// Identificador de la agencia a la que pertenece
        /// </summary>
        public int agenciaId { get; set; }
        /// <summary>
        /// Entidad Agencia a la que pertenece
        /// </summary>
        public Agencia agencia { get; set; }
    }
}

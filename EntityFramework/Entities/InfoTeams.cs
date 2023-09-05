namespace Mensajeria_Linux.EntityFramework.Entities
{
    /// <summary>
    /// Entidad InfoTeams
    /// </summary>
    public class InfoTeams : Base
    {        
        /// <summary>
        /// WebHook de salida
        /// </summary>
        public string webHook { get; set; }
        /// <summary>
        /// Nombre del registro
        /// </summary>
        public string nombre { get; set; }
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

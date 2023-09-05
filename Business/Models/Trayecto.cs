namespace Mensajeria_Linux.Business.Models
{
    /// <summary>
    /// Clase que representa un trayecto
    /// </summary>
    public class Trayecto
    {
        /// <summary>
        /// Fecha de salida
        /// </summary>
        public DateTime fechaSalida { get; set; }
        /// <summary>
        /// Lugar de origen
        /// </summary>
        public string origen { get; set; }
        /// <summary>
        /// Fecha de llegada
        /// </summary>
        public DateTime fechaLlegada { get; set; }
        /// <summary>
        /// Lugar de destino
        /// </summary>
        public string destino { get; set; }
    }
}

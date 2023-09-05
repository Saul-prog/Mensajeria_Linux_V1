
using Microsoft.AspNetCore.Routing.Constraints;

namespace Mensajeria_Linux.Business.Models
{
    /// <summary>
    /// Clase para Gestionar los mensajes de tipo Autorización de datos
    /// </summary>
    public class AutorizacionDatos
    {
        /// <summary>
        /// Identificador de la autorización
        /// </summary>
        public string identificador { get; set; }
        /// <summary>
        /// Nombres de los viajeros que han hecho la reserva
        /// </summary>
        public List<NombreCompleto> viajeroReserva { get; set; }
        /// <summary>
        /// Nombre de los solicitantes de la autorización
        /// </summary>
        public List<NombreCompleto> solicitante { get; set; }
        /// <summary>
        /// Trayectos que se va a efectuar
        /// </summary>
        public List<Trayecto> trayectos { get; set; }
        /// <summary>
        /// Fecha límite para aceptar la autorización
        /// </summary>
        public DateTime fechalimite { get; set; }
        /// <summary>
        /// Improte a autorizar
        /// </summary>
        public float importe { get; set; }
        /// <summary>
        /// Motivo por el cual se necesita la autorización
        /// </summary>
        public string motivo { get; set; }
        /// <summary>
        /// Observaciones sobre al autorización
        /// </summary>
        public string observaciones { get; set; }
        /// <summary>
        /// Datos para redireccionar al destinatario al lugar de aceptación
        /// </summary>
        public string datosRedireccionamiento { get; set; }
    }
}

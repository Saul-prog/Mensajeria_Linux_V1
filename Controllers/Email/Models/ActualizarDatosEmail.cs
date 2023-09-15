using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Linux.Controllers.Email.Models
{
    /// <summary>
    /// Datos para actualziar los datos sobre un email
    /// </summary>
    public class ActualizarDatosEmail
    {
        /// <summary>
        /// Nomrbe de la agencia a la que le pertenecen los datos del email
        /// </summary>
        [Required]
        public string nombreAgencia { get; set; }
        /// <summary>
        /// Token de la agencia a la que le pertenecen los datos del email
        /// </summary>
        public string? tokenAgencia { get; set; }
        /// <summary>
        /// Host que se va a usar para el envío de los email
        /// </summary>
        public string host { get; set; }
       /// <summary>
       /// Puerto que se va a usar para el envío de los email
       /// </summary>
        public int port { get; set; }
       /// <summary>
       /// Correo electrónico que va a enviar los email
       /// </summary>
        public string emailOrigen { get; set; }
        /// <summary>
        /// Token del electrónico que va a enviar los email
        /// </summary>
        public string emailToken { get; set; }
        /// <summary>
        /// Nombre que va a aparecer en los emails enviados
        /// </summary>
        public string nombreEmail { get; set; }
        /// <summary>
        /// Email administrador para la autenticación
        /// </summary>
        public string? emailAdmin { get; set; }
        /// <summary>
        /// Token para la autenticación
        /// </summary>
        public string? tokenAdmin { get; set; }
    }
}

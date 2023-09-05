using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Linux.EntityFramework.Models.Agencias
{
    /// <summary>
    /// Petición para crear una agencia
    /// </summary>
    public class CreateAgenciaRequest
    {
        [Required]
        public string nombreAgencia { get; set; }
        [Required]
        public string token { get; set; }
        [Required]
        public bool puedeEmail { get; set; }
        [Required]
        public bool puedeTeams { get; set; }
        [Required]
        public bool puedeSMS { get; set; }
        [Required]
        public bool puedeWhatsApp { get; set; }
        [Required]
        public int AdminsitradorId { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}

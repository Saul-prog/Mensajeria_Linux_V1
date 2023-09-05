using Mensajeria_Linux.EntityFramework.Entities;
using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Linux.EntityFramework.Models.InfoTeam
{
    /// <summary>
    /// Petición para actualizar un Teams
    /// </summary>
    public class UpdateInfoTeamsRequest
    {
        [Required]
        public string nombreAgencia { get; set; }
        
        public string tokenAgencia { get; set; }
        
        public string webHook { get; set; }
        public string nombre { get; set; }

        public DateTime Updated { get; set; } = DateTime.Now;
    }
}

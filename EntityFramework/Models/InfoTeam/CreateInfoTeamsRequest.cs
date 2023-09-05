using Mensajeria_Linux.EntityFramework.Entities;
using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Linux.EntityFramework.Models.InfoTeam
{
    /// <summary>
    /// Petición para crear un Teams
    /// </summary>
    public class CreateInfoTeamsRequest
    {
        [Required]
        public string nombreAgencia { get; set; }
        [Required]
        public string tokenAgencia { get; set; }
        [Required]
        public string webHook { get; set; }
        [Required]
        public string nombre { get; set; }
        

        public DateTime Created { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MVC_Test2.Entities.Request
{
    public class PutUltimaConexionRequest
    {
        [Required]
        public string ultimaConexion { get; set; }
    }
}

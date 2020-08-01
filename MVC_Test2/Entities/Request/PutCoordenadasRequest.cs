using System.ComponentModel.DataAnnotations;

namespace MVC_Test2.Entities.Request
{
    public class PutCoordenadasRequest
    {
        [Required]
        public decimal longitud { get; set; }
        [Required]
        public decimal latitud { get; set; }
    }
}

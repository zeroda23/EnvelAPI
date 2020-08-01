using System.ComponentModel.DataAnnotations;

namespace MVC_Test2.Entities.Request
{
    public class GetChatRequest
    {
        [Required]
        public string id { get; set; }
        [Required]
        public decimal longitud { get; set; }
        [Required]
        public decimal latitud { get; set; }
    }
}

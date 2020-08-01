using System.ComponentModel.DataAnnotations;

namespace MVC_Test2.Entities.Request
{
    public class PutPasswordRequest
    {
        [Required]
        public string password { get; set; }
    }
}

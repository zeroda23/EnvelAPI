using System.Collections.Generic;

namespace MVC_Test2.Request
{
    public class PostUsuarioRequest
    {
        public string id { get; set; }
        public string usuario { get; set; }
        public decimal longitud { get; set; }
        public decimal latitud { get; set; }
        public string rolId { get; set; }
        public string rolNombre { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public string pass { get; set; }
        public string estatus { get; set; }
        public string descripcion { get; set; }
        public List<string> rolesPreferentes { get; set; }
    }
}

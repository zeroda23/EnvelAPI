using System.Collections.Generic;

namespace MVC_Test2.Request
{
    public class PutUsuarioRequest
    {        
        public string usuario { get; set; }
        public string rolId { get; set; }
        public string rolNombre { get; set; }
        public string descripcion { get; set; }
        public List<string> rolesPreferentes { get; set; }
    }
}

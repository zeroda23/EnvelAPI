using MVC_Test2.Entities.Database;
using System.Collections.Generic;

namespace MVC_Test2.Response
{
    public class GetUsuarioResponse
    {
        public GetUsuarioResponse()
        {
            matches = new List<ItemMatch>();
        }

        public string id { get; set; }
        public string usuario { get; set; }
        public string rolId { get; set; }
        public string rolNombre { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public string pass { get; set; }
        public string ultimaConexion { get; set; }
        public string estatus { get; set; }
        public string descripcion { get; set; }
        public decimal limiteBusqueda { get; set; }
        public decimal longitud { get; set; }
        public decimal latitud { get; set; }
        public List<ItemMatch> matches { get; set; }
        public List<string> rolesPreferentes { get; set; }
    }
}

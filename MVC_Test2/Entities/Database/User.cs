using MVC_Test2.Entities.Database;
using System.Collections.Generic;

namespace MVC_Test2.Entities.DataBase
{
    public class User: EncabezadoDocumento
    {
        public User()
        {
            geometry = new Geometry();
            limiteBusqueda = 0.050000M;
            matches = new List<ItemMatch>();
        }

        public Geometry geometry { get; set; }
        public string id { get; set; }
        public string usuario { get; set; }
        public string rolId { get; set; }
        public string rolNombre { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public string pass { get; set; }
        public string fechaCreacion { get; set; }
        public string ultimaConexion { get; set; }
        public string estatus { get; set; }
        public string descripcion { get; set; }
        public decimal limiteBusqueda { get; set; }
        public List<ItemMatch> matches { get; set; }
        public List<string> rolesPreferentes { get; set; }       
    }
}

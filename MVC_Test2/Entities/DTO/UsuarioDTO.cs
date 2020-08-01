using MVC_Test2.Entities.Database;
using System.Collections.Generic;

namespace MVC_Test2.Entities.DTO
{
    public class UsuarioDTO: EncabezadoDocumento
    {
        public UsuarioDTO()
        {
            geometry = new Geometry();
            matches = new List<ItemMatchDTO>();
        }

        public Geometry geometry { get; set; }

        public string key { get; set; }
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
        public List<ItemMatchDTO> matches { get; set; }
        public List<string> rolesPreferentes { get; set; }
    }

    public class ItemMatchDTO
    {
        public string id { get; set; }
        public bool filtrado { get; set; }
    }
}


using System.Collections.Generic;

namespace MVC_Test2.Entities.DataBase
{
    public class Encabezado
    { 
        public Encabezado()
        {
            Rows = new List<Row>();
        }

        public int Total_rows { get; set; }
        public int Offset { get; set; }
        public List<Row> Rows { get; set; }
    }
    public class Row
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public object Doc { get; set; }
    } 
}

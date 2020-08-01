namespace MVC_Test2.Entities.Database
{
    public class Geometry
    {
        public Geometry()
        {
            type = "Point";
            coordinates = new decimal[2];
        }

        public decimal [] coordinates { get; set; }
        public string type { get; }
    }
}

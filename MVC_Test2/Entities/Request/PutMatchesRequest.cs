using System.Collections.Generic;

namespace MVC_Test2.Entities.Request
{
    public class PutMatchesRequest
    {
        public PutMatchesRequest() => matches = new List<ItemMatchRequest>();

        public List<ItemMatchRequest> matches { get; set; }
    }

    public class ItemMatchRequest
    {
        public string id { get; set; }
        public bool filtrado { get; set; }
    }
}

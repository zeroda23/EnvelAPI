using System.Collections.Generic;

namespace MVC_Test2.Request
{
    public class GetTipsRequest
    {
        public GetTipsRequest() => Tips = new List<GetTipRequest>();
        public List<GetTipRequest> Tips { get; set; }
    }
}

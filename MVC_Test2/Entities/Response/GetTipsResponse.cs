using System.Collections.Generic;

namespace MVC_Test2.Response
{
    public class GetTipsResponse
    {
        public GetTipsResponse() => Tips = new List<GetTipResponse>();
        public List<GetTipResponse> Tips { get; set; }
    }
}

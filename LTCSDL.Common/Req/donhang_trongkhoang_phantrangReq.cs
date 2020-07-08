using System;
using System.Collections.Generic;
using System.Text;

namespace LTCSDL.Common.Req
{
    public class donhang_trongkhoang_phantrangReq
    {
        public DateTime begin { get; set; }
        public DateTime ending { get; set; }
        public int page { get; set; }
        public int size { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace LTCSDL.Common.Req
{
    public class Insert_Record_SupReq
    {
        public string CompanyName { get; set; }
        public string Region { get; set; }
        public int page { get; set; }
        public int size { get; set; }

    }
}

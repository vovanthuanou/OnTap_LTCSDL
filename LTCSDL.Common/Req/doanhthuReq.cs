using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace LTCSDL.Common.Req
{
    public class doanhthuReq
    {
        public DateTime date;

        public DateTime dateF { get; set; }
        public DateTime dateT{ get; set; }
        public int th { get; set; }
        public string ten { get; set; }
        public DateTime begin { get; set; }
        public DateTime ending { get; set; }
        public int page { get; set; }
        public int size { get; set; }
    }
}

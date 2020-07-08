using Newtonsoft.Json;

using LTCSDL.BLL;
using LTCSDL.Common.Rsp;
using LTCSDL.Common.BLL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LTCSDL.BLL
{
    using DAL;
    using DAL.Models;
    using LTCSDL.Common.Req;

    public class ShippersSvc : GenericSvc<ShippersRep, Shippers>
    {
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();

            var m = _rep.Read(id);
            res.Data = m;

            return res;
        }

        public SingleRsp CreateShippers(ShippersReq ship)
        {
            var res = new SingleRsp();
            Shippers shipper = new Shippers();
            shipper.ShipperId = ship.ShipperId;
            shipper.CompanyName = ship.CompanyName;
            shipper.Phone = ship.Phone;

            res = _rep.CreateShippers(shipper);
            return res;
        }

        public SingleRsp UpdateShippers(ShippersReq ship)
        {
            var res = new SingleRsp();
            Shippers shipper = new Shippers();
            shipper.ShipperId = ship.ShipperId;
            shipper.CompanyName = ship.CompanyName;
            shipper.Phone = ship.Phone;

            res = _rep.UpdateShippers(shipper);
            return res;
        }
        public SingleRsp DeleteShipper(int id)
        {
            var res = new SingleRsp();
            try
            {
                res.Data = _rep.DeleteShipper(id);

            }
            catch (Exception ex)
            {
                res.SetError(ex.StackTrace);
            }
            return res;
        }
        public object insert_Record_Sup(string company, string contry, int page, int size)
        {
            return _rep.insert_Record_Sup(company, contry, page, size);
        }
    }
}

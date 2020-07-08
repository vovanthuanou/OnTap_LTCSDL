using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LTCSDL.Web.Controllers
{
    using BLL;
    using DAL.Models;
    using Common.Req;
    using System.Collections.Generic;
    //using BLL.Req;
    using Common.Rsp;

    [Route("api/[controller]")]
    [ApiController]
    public class ShippersController : Controller
    {
        public ShippersController()
        {
            _svc = new ShippersSvc();
        }

        [HttpPost("create_shippers")]
        public IActionResult CreateShippers([FromBody] ShippersReq req)
        {
            var res = _svc.CreateShippers(req);

            return Ok(res);
        }

        [HttpPost("update_shippers")]
        public IActionResult UpdateShippers([FromBody] ShippersReq req)
        {
            var res = _svc.UpdateShippers(req);

            return Ok(res);
        }

        [HttpPost("delete_shippers")]
        public IActionResult DeleteShipper([FromBody] DeleteSipperReq req)
        {
            var res = _svc.DeleteShipper(req.ShipperId);

            return Ok(res);
        }

        [HttpPost("insert-record-sup")]
        public IActionResult insert_Record_Sup([FromBody] Insert_Record_SupReq req)
        {
            var res = _svc.insert_Record_Sup(req.CompanyName, req.Region, req.page, req.size);

            return Ok(res);
        }


        private readonly ShippersSvc _svc;
    }
}
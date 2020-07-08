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
    public class CategoriesController : ControllerBase
    {
        public CategoriesController()
        {
            _svc = new CategoriesSvc();
        }

        [HttpPost("get-by-id")]
        public IActionResult getCategoryById([FromBody]SimpleReq req)
        {
            var res = new SingleRsp();
            res = _svc.Read(req.Id);
            return Ok(res);
        }

        [HttpPost("get-all")]
        public IActionResult getAllCategories()
        {
            var res = new SingleRsp();
            res.Data = _svc.All;
            return Ok(res);
        }

        [HttpPost("get-cust-order-hist")]
        public IActionResult getCustOrderHist([FromBody]SimpleReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.getCustOrderList(req.Keyword);
            return Ok(res);
        }

        [HttpPost("get-cust-order-detail")]
        public IActionResult getCustOrderDetail([FromBody]SimpleReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.getCustOrderDetail(req.Id);
            return Ok(res);
        }

        [HttpPost("get-cust-order-hist-linq")]
        public IActionResult getCustOrderList_LinQ([FromBody]SimpleReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.getCustOrderList_LinQ(req.Keyword);
            return Ok(res);
        }

        [HttpPost("get-cust-order-details-linq")]
        public IActionResult getCustOrderDetail_LinQ([FromBody]SimpleReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.getCustOrderDetail_LinQ(req.Id);
            return Ok(res);
        }

        // de 1
        [HttpPost("get-doanhthutrongngay")] //2a
        public IActionResult getDoanhThuTrongNgay([FromBody]doanhthutrongngayRep req)
        {
            var res = new SingleRsp();
            res.Data = _svc.getDoanhThuTrongNgay(req.date);
            return Ok(res);
        }

        [HttpPost("get-doanhthutrongngay-linq")]//3a
        public IActionResult getdoanhthutrongngay_LinQ([FromBody]doanhthutrongngayRep req)
        {
            var res = new SingleRsp();
            res.Data = _svc.getdoanhthutrongngay_LinQ(req.date);
            return Ok(res);
        }

        [HttpPost("get-doanh-thu-trong-khoang")]//2b
        public IActionResult getdoanhthutrongkhoangtg([FromBody]doanhthutrongkhoangReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.getdoanhthutrongkhoangtg(req.dateF, req.dateT);
            return Ok(res);
        }


        [HttpPost("get-doanhthutrongkhoangtg-linq")]//3b
        public IActionResult getdoanhthutrongkhoangtg_LinQ([FromBody]doanhthutrongkhoangReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.getdoanhthutrongkhoangtg_LinQ(req.dateF, req.dateT);
            return Ok(res);
        }

        //de 2
        [HttpPost("get-don-hang-trong-khoang-phan-trang")]//2a
        public IActionResult getdonhangtrongkhoangtg_phantrang([FromBody]donhang_trongkhoang_phantrangReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.getdonhangtrongkhoangtg_phantrang(req.begin, req.ending, req.page, req.size);
            return Ok(res);
        }

        [HttpPost("get-don-hang-trong-khoang-phan-trang-linq")]//2b
        public IActionResult getdonhangtrongkhoangtg_phantrang_LinQ([FromBody]donhang_trongkhoang_phantrangReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.getdonhangtrongkhoangtg_phantrang_LinQ(req.begin, req.ending, req.page, req.size);
            return Ok(res);
        }

        [HttpPost("get-order-ordedetail")]//3a
        public IActionResult getOrder_OrderDetail([FromBody]donhangRep req)
        {
            var res = new SingleRsp();
            res.Data = _svc.getOrder_OrderDetail(req.OrderID);
            return Ok(res);
        }

        [HttpPost("get-order-ordedetail-linq")]//3b
        public IActionResult getdonhang_LinQ([FromBody]donhangRep req)
        {
            var res = new SingleRsp();
            res.Data = _svc.getOrder_OrderDetail(req.OrderID);
            return Ok(res);
        }

        //de3
        [HttpPost("get-doanh-thu_phantrang")]//2a
        public IActionResult get_Name_OrderBy_PhanTrang([FromBody]Doanhthu1 req)
        {
            var res = new SingleRsp();
            res.Data = _svc.get_Name_OrderBy_PhanTrang(req.ten, req.begin,req.ending,req.page,req.size);
            return Ok(res);
        }

        [HttpPost("get-top10-salse-product")]//2b
        public IActionResult get_top10_salepro([FromBody]Top10doanhthuReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.get_top10_salepro(req.thang, req.nam, req.truonghop);
            return Ok(res);
        }

        [HttpPost("get-doanhthu-customer-region")]//2c
        public IActionResult getdoanhthu_cusregion([FromBody]Doanhthu_Thang_NamReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.getdoanhthu_cusregion(req.thang, req.nam);
            return Ok(res);
        }

        private readonly CategoriesSvc _svc;
    }
}
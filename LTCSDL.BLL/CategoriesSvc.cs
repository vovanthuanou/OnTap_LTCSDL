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

    public class CategoriesSvc : GenericSvc<CategoriesRep, Categories>
    {
        #region -- Overrides --

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();

            var m = _rep.Read(id);
            res.Data = m;

            return res;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="m">The model</param>
        /// <returns>Return the result</returns>
        public override SingleRsp Update(Categories m)
        {
            var res = new SingleRsp();

            var m1 = m.CategoryId > 0 ? _rep.Read(m.CategoryId) : _rep.Read(m.Description);
            if (m1 == null)
            {
                res.SetError("EZ103", "No data.");
            }
            else
            {
                res = base.Update(m);
                res.Data = m;
            }

            return res;
        }
        #endregion

        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public CategoriesSvc() { }


        #endregion

        public object getCustOrderList(string cusID)
        {
            return _rep.getCustOrderList(cusID);
            // sau đó qua lớp Rsp
        }

        public object getCustOrderDetail(int odrID)
        {
            return _rep.getCustOrdersDetail(odrID);
        }
        // sau đó qua lớp Rsp

        public object getCustOrderList_LinQ(string custID)
        {
            return _rep.getCustOrderList_LinQ(custID);
        }
      

        public object getCustOrderDetail_LinQ(int odrID)
        {
            return _rep.getCustOrderDetail_LinQ(odrID);
        }
        

        //de1
        public object getdoanhthutrongkhoangtg(DateTime dateF, DateTime dateT)
        {
            return _rep.getdoanhthutrongkhoangtg(dateF, dateT);
        }//2b
       

        public object getDoanhThuTrongNgay(DateTime date)
        {
            return _rep.getDoanhThuTrongNgay(date);
        }//2a
       

        public object getdoanhthutrongngay_LinQ(DateTime dateF)
        {
            return _rep.getdoanhthutrongngay_LinQ(dateF);
        }//3a
        

        public object getdoanhthutrongkhoangtg_LinQ(DateTime dateF, DateTime dateT)
        {
            return _rep.getdoanhthutrongkhoangtg_LinQ(dateF, dateT);
        }//3b
        

        //de 2
        public object getdonhangtrongkhoangtg_phantrang(DateTime dateF, DateTime dateT, int page, int size)
        {
            return _rep.getdonhangtrongkhoangtg_phantrang(dateF, dateT, page, size);
        } //2a
       

        public object getdonhangtrongkhoangtg_phantrang_LinQ(DateTime dateF, DateTime dateT, int page, int size)
        {
            return _rep.getdonhangtrongkhoangtg_phantrang_LinQ(dateF, dateT, page, size);
        } //3a

        public object getOrder_OrderDetail(int id)
        {
            return _rep.getOrder_OrderDetail(id);
        }//2b

        public object getdonhang_LinQ(int id)
        {
            return _rep.getdonhang_LinQ(id);
        } //3b

        // de 3
        public object get_Name_OrderBy_PhanTrang(string ten, DateTime begin, DateTime ending, int page, int size)
        {
            return _rep.get_Name_OrderBy_PhanTrang(ten, begin, ending, page, size);
        } //2a

        public object get_top10_salepro(int m, int y, int x)//2b
        {
            return _rep.get_top10_salepro(m, y, x);
        }

        public object getdoanhthu_cusregion(int m, int y)//2c
        {
            return _rep.getdoanhthu_cusregion(m, y);
        }


    }
}

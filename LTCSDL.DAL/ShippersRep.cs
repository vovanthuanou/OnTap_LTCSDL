using LTCSDL.Common.DAL;
using System.Linq;

namespace LTCSDL.DAL
{
    using LTCSDL.Common.Rsp;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Reflection.Metadata.Ecma335;
    using System.Xml.Schema;

    public class ShippersRep : GenericRep<NorthwindContext, Shippers>
    {
        #region -- Overrides --

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public override Shippers Read(int id)
        {
            var res = All.FirstOrDefault(p => p.ShipperId == id);
            return res;
        }


        /// <summary>
        /// Remove and not restore
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Number of affect</returns>
        public int Remove(int id)
        {
            var m = base.All.First(i => i.ShipperId == id);
            m = base.Delete(m); //TODO
            return m.ShipperId;
        }

        #endregion


        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        /// 
        // thay vì làm ado.net có sằn trong sql thì làm bằng Lin_Q
        public SingleRsp CreateShippers(Shippers spp)
        {
            var res = new SingleRsp();
            using (var context = new NorthwindContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var T = context.Shippers.Add(spp);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }
        #endregion
        public SingleRsp UpdateShippers(Shippers spp)
        {
            var res = new SingleRsp();
            using (var context = new NorthwindContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var T = context.Shippers.Update(spp);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }
        public int DeleteShipper(int id)
        {
            var m = base.All.First(i => i.ShipperId == id);
            Context.Shippers.Remove(m);
            Context.SaveChanges();
            return m.ShipperId;
        }
        public object insert_Record_Sup(string company, string contry, int page, int size)
        {
            List<object> res = new List<object>();
            var cnn = (SqlConnection)(Context.Database.GetDbConnection());
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet(); //để hứng data
                var cmd = cnn.CreateCommand();
                cmd.CommandText = "Sup_Search_Phantrang";
                cmd.CommandType = CommandType.StoredProcedure; //để nhận biết nó là store
                cmd.Parameters.AddWithValue("@com_name", company);
                cmd.Parameters.AddWithValue("@con", contry);
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@size", size);
                //lấy dữ liệu đổ vào dataset
                da.SelectCommand = cmd; //lấy
                da.Fill(ds); //đổ
                //kiểm tra có dữ liệu để đổ, sl table >0 và cái table đó có số dòng >0 thì mới trả về dữ liệu
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    // duyệt từng dòng 1
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        //tạo list đối tượng để trả về
                        var x = new
                        {
                            SupplierID = row["SupplierID"],
                            CompanyName = row["CompanyName"],
                            ContactName = row["ContactName"],
                            ContactTitle = row["ContactTitle"],
                            Country = row["Country"]
                        };
                        res.Add(x); //trả về đối tượng x

                    }
                }
            }
            catch (Exception)
            {
                res = null;
            }
            return res;
        }
    }
}
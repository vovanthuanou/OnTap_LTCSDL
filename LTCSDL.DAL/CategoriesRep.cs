using LTCSDL.Common.DAL;
using System.Linq;

namespace LTCSDL.DAL
{
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Reflection.Metadata.Ecma335;
    using System.Xml.Schema;

    public class CategoriesRep : GenericRep<NorthwindContext, Categories>
    {
        #region -- Overrides --

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public override Categories Read(int id)
        {
            var res = All.FirstOrDefault(p => p.CategoryId == id);
            return res;
        }


        /// <summary>
        /// Remove and not restore
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Number of affect</returns>
        public int Remove(int id)
        {
            var m = base.All.First(i => i.CategoryId == id);
            m = base.Delete(m); //TODO
            return m.CategoryId;
        }

        #endregion


        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        /// 
        // thay vì làm ado.net có sằn trong sql thì làm bằng Lin_Q
        public object getCustOrderList_LinQ(string cusID)
        {
            var res = Context.Products
                .Join(Context.OrderDetails, a => a.ProductId, b => b.ProductId, (a, b) => new
                {
                    a.ProductId,
                    a.ProductName,
                    b.Quantity,
                    b.OrderId
                })
                .Join(Context.Orders, a => a.OrderId, b => b.OrderId, (a, b) => new
                {
                    a.ProductId,
                    a.ProductName,
                    a.OrderId,
                    a.Quantity,
                    b.CustomerId
                }).Where(x => x.CustomerId == cusID).ToList();
            var data = res.GroupBy(x => x.ProductName).Select(x => new
            {
                ProductName = x.First().ProductName,
                Total = x.Sum(p => p.Quantity)
            });
            return data;
        }

        //làm LinQ

        public object getCustOrderDetail_LinQ(int odrID)
        {
            var res = Context.OrderDetails
                .Join(Context.Products, a => a.ProductId, b => b.ProductId, (a, b) => new
                {
                    a.Quantity,
                    a.UnitPrice,
                    a.Discount,
                    a.OrderId,
                    b.ProductId,
                    b.ProductName,
                    ExtendedPrice = a.Quantity * (1 - (decimal)a.Discount) * a.UnitPrice
                }).Where(x => x.OrderId == odrID).ToList();
            return res;
        }

        // làm ado.net để lấy storeprocedure trong sql
        public object getCustOrderList(string cusID)
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
                cmd.CommandText = "CustOrderHist";
                cmd.CommandType = CommandType.StoredProcedure; //để nhận biết nó là store
                cmd.Parameters.AddWithValue("@CustomerID", cusID);
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
                            ProductName = row["ProductName"],
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
            // sau khi xong sẽ viết qua lớp BLL
        }

        //làm ado.net để lấy store procedure
        public object getCustOrdersDetail(int odrID)
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
                cmd.CommandText = "CustOrdersDetail";
                cmd.CommandType = CommandType.StoredProcedure; //để nhận biết nó là store
                cmd.Parameters.AddWithValue("@OrderID", odrID);
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
                            ProductName = row["ProductName"],
                            UnitPrice = row["UnitPrice"],
                            Quantity = row["Quantity"],
                            Discount = row["Discount"],
                            ExtendedPrice = row["ExtendedPrice"],
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
            // sau khi xong sẽ viết qua lớp BLL
        }

        // de1
        public object getDoanhThuTrongNgay(DateTime date)
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
                cmd.CommandText = "getDoanhThuTrongNgay";
                cmd.CommandType = CommandType.StoredProcedure; //để nhận biết nó là store
                cmd.Parameters.AddWithValue("@date", date);
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
                            EmployeeID = row["EmployeeID"],
                            LastName = row["LastName"],
                            FirstName = row["FirstName"],
                            revenue = row["revenue"]
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
        } //2a

        public object getdoanhthutrongngay_LinQ(DateTime date)
        {
            var res = Context.Orders
                .Join(Context.OrderDetails, a => a.OrderId, b => b.OrderId, (a, b) => new
                {
                    a.OrderId,
                    a.EmployeeId,
                    a.OrderDate,
                    b.Quantity,
                    b.Discount,
                    b.UnitPrice,
                    doanhthu = (b.Quantity * (decimal)(1 - b.Discount) * b.UnitPrice)
                })
                .Join(Context.Employees, a => a.EmployeeId, b => b.EmployeeId, (a, b) => new
                {
                    a.EmployeeId,
                    a.doanhthu,
                    a.OrderDate,
                    b.FirstName,
                    b.LastName
                }).Where(x => x.OrderDate == date).ToList();
            var data = res.GroupBy(x => x.EmployeeId).Select(x => new
            {
                FirstName = x.First().FirstName,
                Total = x.Sum(p => p.doanhthu)
            }); ;
            return data;
        } //3a

        public object getdoanhthutrongkhoangtg(DateTime dateF, DateTime dateT)
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
                cmd.CommandText = "doanhthutrongkhoangtg";
                cmd.CommandType = CommandType.StoredProcedure; //để nhận biết nó là store
                cmd.Parameters.AddWithValue("@start", dateF);
                cmd.Parameters.AddWithValue("@end", dateT);
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
                            LastName = row["LastName"],
                            FirstName = row["FirstName"],
                            revenue = row["revenue"]
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
        }//2b

        public object getdoanhthutrongkhoangtg_LinQ(DateTime dateF, DateTime dateT)
        {
            var res = Context.Orders
                .Join(Context.OrderDetails, a => a.OrderId, b => b.OrderId, (a, b) => new
                {
                    a.OrderId,
                    a.EmployeeId,
                    a.OrderDate,
                    b.Quantity,
                    b.Discount,
                    b.UnitPrice,
                    doanhthu = (b.Quantity * (decimal)(1 - b.Discount) * b.UnitPrice)
                })
                .Join(Context.Employees, a => a.EmployeeId, b => b.EmployeeId, (a, b) => new
                {
                    a.EmployeeId,
                    a.doanhthu,
                    a.OrderDate,
                    b.FirstName,
                    b.LastName
                }).Where(x => x.OrderDate >= dateF && x.OrderDate <= dateT).ToList();
            var data = res.GroupBy(x => x.EmployeeId).Select(x => new
            {
                x.First().LastName,
                x.First().FirstName,
                revenue = x.Sum(p => p.doanhthu)
            });
            return data;

            #endregion
        }//3b

        //de 2
        public object getdonhangtrongkhoangtg_phantrang(DateTime dateF, DateTime dateT, int page, int size)
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
                cmd.CommandText = "KhoangthoigianOder_Phantrang";
                cmd.CommandType = CommandType.StoredProcedure; //để nhận biết nó là store
                cmd.Parameters.AddWithValue("@begin", dateF);
                cmd.Parameters.AddWithValue("@ending", dateT);
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
                            OrderID = row["OrderID"],
                            CustomerID = row["CustomerID"],
                            EmployeeID = row["EmployeeID"],
                            OrderDate = row["OrderDate"],
                            Freight = row["Freight"]
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
        }//2a

        public object getdonhangtrongkhoangtg_phantrang_LinQ(DateTime dateF, DateTime dateT, int page, int size)
        {
            //khong can ket bang
            var res = Context.Orders.Where(x => x.OrderDate >= dateF && x.OrderDate <= dateT).Select(x => new
            {
                x.OrderDate,
                x.CustomerId,
                x.EmployeeId,
                x.OrderId
            }).ToList();
            //phan trang
            var offset = (page - 1) * size;
            var total = res.Count();
            int totalPages = (total % size) == 0 ? (int)(total / size) : (int)((total / size) + 1);
            var data = res.Skip(offset).Take(size).ToList();
            var res1 = new
            {
                Data = data,
                TotalRecord = total,
                TotalPages = totalPages,
                Page = page,
                Size = size
            };
            return res1;

        }//3a
        public object getOrder_OrderDetail(int id)
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
                cmd.CommandText = "getOrder_OrderDetail";
                cmd.CommandType = CommandType.StoredProcedure; //để nhận biết nó là store
                cmd.Parameters.AddWithValue("@manv", id);
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
                            OrderID = row["OrderID"],
                            ProductID = row["ProductID"],
                            UnitPrice = row["UnitPrice"],
                            Quantity = row["Quantity"],
                            Discount = row["Discount"],
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
            // sau khi xong sẽ viết qua lớp BLL
        }//2b

        public object getdonhang_LinQ(int id)
        {
            //khong can ket bang
            var res = Context.OrderDetails
                .Join(Context.Orders, a => a.OrderId, b => b.OrderId, (a, b) => new
                {
                    a.ProductId,
                    b.OrderId,
                    a.UnitPrice,
                    a.Quantity,
                    a.Discount
                }).Where(x => x.OrderId == id).Select(x => new
                {
                    x.OrderId,
                    x.ProductId,
                    x.UnitPrice,
                    x.Quantity,
                    x.Discount
                }).ToList();
 
            return res;
        }//3b

        //de 3
         public object get_Name_OrderBy_PhanTrang(string ten, DateTime begin, DateTime ending,int page, int size)
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
                cmd.CommandText = "SearchName_Datef_DateT_OrderByandPhanTrang";
                cmd.CommandType = CommandType.StoredProcedure; //để nhận biết nó là store
                cmd.Parameters.AddWithValue("@name", ten);
                cmd.Parameters.AddWithValue("@begin", begin);
                cmd.Parameters.AddWithValue("@ending", ending);
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
                            EmployeeID = row["EmployeeID"],
                            LastName = row["LastName"]
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
        } //2a

        public object get_top10_salepro(int m, int y, int x)
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
                cmd.CommandText = "get_product_10";
                cmd.CommandType = CommandType.StoredProcedure; //để nhận biết nó là store
                cmd.Parameters.AddWithValue("@month", m);
                cmd.Parameters.AddWithValue("@year", y);
                cmd.Parameters.AddWithValue("@x", x);
                //lấy dữ liệu đổ vào dataset
                da.SelectCommand = cmd; //lấy
                da.Fill(ds); //đổ
                //kiểm tra có dữ liệu để đổ, sl table >0 và cái table đó có số dòng >0 thì mới trả về dữ liệu
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (x == 0) // truong hop theo so luong
                    {
                        // duyệt từng dòng 1
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            //tạo list đối tượng để trả về
                            var i = new
                            {
                                ProductID = row["ProductID"],
                                ProductName = row["ProductName"],
                                Quantity = row["Quantity"]
                            };
                            res.Add(i); //trả về đối tượng x
                        }
                    }
                    else
                    {
                        if (x == 1)
                        {
                            // duyệt từng dòng 1
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                //tạo list đối tượng để trả về
                                var i = new
                                {
                                    ProductID = row["ProductID"],
                                    ProductName = row["ProductName"],
                                    revenue = row["revenue"]
                                };
                                res.Add(i); //trả về đối tượng x

                            }

                        }
                    }
                }
            }
            catch (Exception)
            {
                res = null;
            }
            return res;
        }//2b

        public object getdoanhthu_cusregion(int m, int y) //2c
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
                cmd.CommandText = "getdoanhthu_cusregion";
                cmd.CommandType = CommandType.StoredProcedure; //để nhận biết nó là store
                cmd.Parameters.AddWithValue("@month", m);
                cmd.Parameters.AddWithValue("@year", y);
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
                        var i = new
                        {
                            Region = row["Region"],
                            revenue = row["revenue"],
                        };
                        res.Add(i); //trả về đối tượng x
                    }
                }
            }
            catch (Exception)
            {
                res = null;
            }
            return res;
        }//2b
    }
}

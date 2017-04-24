using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleManagementSys
{
    class Database
    {
        SqlConnection conn;

        public Database()
        {
            conn = new SqlConnection(@"Server=tcp:mike-jac.database.windows.net,1433;Initial Catalog=POS;
Persist Security Info=False;User ID=dbadmin;Password=Elmira2000;MultipleActiveResultSets=False;
Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            conn.Open();
        }

        public List<Categories> GetAllCategory()
        {
            List<Categories> result = new List<Categories>();
            using (SqlCommand command = new SqlCommand("SELECT * FROM categories", conn))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = (int)reader["CategoryId"];
                    string name = (string)reader["categoryName"];
                    Categories p = new Categories(id, name);
                    result.Add(p);
                }
            }
            return result;
        }

        public Shopping GetProductbyId(int CategoryId, int ProductId)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM Products where CategoryId=" + CategoryId + " and productId=" + ProductId, conn))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    string name = (string)reader["productName"];
                    decimal price = (decimal)reader["unitprice"];
                    int Id = (int)reader["productId"];
                    // Console.WriteLine(name + "  " + price);
                    Shopping p = new Shopping(Id, name, 1, price, 4, 3, 1);
                    return p;
                }
                return null;
            }
        }

        public List<OrderList> GetAllOrderList(int Id)
        {
            List<OrderList> result = new List<OrderList>();
            using (SqlCommand command = new SqlCommand("SELECT * FROM OrderList where orderId="+Id, conn))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int orderId = (int)reader["orderId"];
                    int productId = (int)reader["productId"];
                    int quantity = (int)reader["quantity"];
                    decimal unitprice = (decimal)reader["unitprice"];
                    OrderList o = new OrderList(orderId, productId, quantity, unitprice, 0.1m);
                    result.Add(o);
                }
            }
            return result;
        }

        public List<Order> GetAllOrders()
        {
            List<Order> result = new List<Order>();
            using (SqlCommand command = new SqlCommand("SELECT * FROM Orders", conn))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int orderId = (int)reader["orderId"];
                    int empId = (int)reader["empId"];
                    DateTime orderDate = (DateTime)reader["orderDate"];
                    int customerId = (int)reader["customerId"];
                    decimal totalPrice = (decimal)reader["totalPrice"];
                    string paymentMethod = (string)reader["paymentMethod"];
                    int invoiceNr = (int)reader["invoiceNr"];
                    Order o = new Order(orderId, empId, orderDate, customerId, totalPrice, paymentMethod, invoiceNr);
                    result.Add(o);
                }
            }
            return result;
        }

        public int MaxOrderId()
        {
            int maxOrderId = 0;
            using (SqlCommand command = new SqlCommand("SELECT MAX(orderId) as maxId FROM Orders", conn))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    maxOrderId = (int)reader["maxId"];
                }
            }
            return maxOrderId;
        }


        public void AddOrder(Order o)
        {
            string sql = "INSERT INTO Orders (orderId, empId, orderDate,customerId, totalPrice, paymentMethod, invoiceNr) VALUES (@orderId, @empId, @orderDate, @customerId, @totalPrice, @paymentMethod, @invoiceNr)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@orderId", SqlDbType.Int).Value = o.OrderId;
            cmd.Parameters.Add("@empId", SqlDbType.Int).Value = o.EmpId;
            cmd.Parameters.Add("@orderDate", SqlDbType.DateTime).Value = o.OrderDate;
            cmd.Parameters.Add("@customerId", SqlDbType.Int).Value = o.CustomerId;
            cmd.Parameters.Add("@paymentMethod", SqlDbType.Text).Value = o.PaymentMethod;
            cmd.Parameters.Add("@totalPrice", SqlDbType.Money).Value = o.TotalPrice;
            cmd.Parameters.Add("@invoiceNr", SqlDbType.Int).Value = o.InvoiceNr;
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }

        public void UpdateOrderList(OrderList o)
        {
            using (SqlCommand cmd = new SqlCommand(
            "UPDATE OrderList SET quantity = @quantity WHERE productId=@productId and orderId=@orderId", conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = o.Quantity;
                cmd.Parameters.Add("@orderId", SqlDbType.Int).Value = o.OrderId;
                cmd.Parameters.Add("@productId", SqlDbType.Int).Value = o.ProductId;
                cmd.ExecuteNonQuery();
            }
        }

        public void AddOrderList(OrderList o)
        {
            string sql = "INSERT INTO Orderlist (orderId, productId, quantity, unitPrice) VALUES (@orderId, @productId, @quantity, @unitPrice)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@orderId", SqlDbType.Int).Value = o.OrderId;
            cmd.Parameters.Add("@productId", SqlDbType.Int).Value = o.ProductId;
            cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = o.Quantity;
            cmd.Parameters.Add("@unitPrice", SqlDbType.Decimal).Value = o.UnitPrice;
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }



        public void DeleteOrderListById(int Id)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM orderlist WHERE productId=@Id", conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteOrderById(int Id)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM orderlist WHERE orderId=@Id", conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.ExecuteNonQuery();
            }
        }

    }
}

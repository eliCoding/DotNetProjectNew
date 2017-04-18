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

        public Shopping GetProductbyID(int CategoryId, int ProductId)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM Products where CategoryId="+CategoryId+" and productId="+ProductId, conn))
            using (SqlDataReader reader = command.ExecuteReader())

            {
                if (reader.Read())
                {
                    string name = (string)reader["productName"];
                    decimal price = (decimal)reader["unitprice"];
                    Console.WriteLine(name + "  " + price);
                    Shopping p= new Shopping(name, 1, price, 4, 3, 1);
                    return p;
                }
                return null;
            }
           
        }


        //public void AddPerson(Person p)
        //{
        //    string sql = "INSERT INTO people (Name, Age) VALUES (@Name, @Age)";
        //    SqlCommand cmd = new SqlCommand(sql, conn);
        //    cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = p.Name;
        //    cmd.Parameters.Add("@Age", SqlDbType.Int).Value = p.Age;
        //    cmd.CommandType = CommandType.Text;
        //    cmd.ExecuteNonQuery();
        //}


        //public void DeletePersonById(int Id)
        //{
        //    // Console.WriteLine("jjjjjjj");
        //    using (SqlCommand cmd = new SqlCommand("DELETE FROM People WHERE Id=@Id", conn))
        //    {
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.Parameters.AddWithValue("@Id", Id);
        //        cmd.ExecuteNonQuery();
        //    }
        //}

        //public void UpdatePerson(Person p)
        //{
        //    using (SqlCommand cmd = new SqlCommand(
        //        "UPDATE People SET Name = @Name, Age = @Age WHERE Id=@Id", conn))
        //    {
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.Parameters.AddWithValue("@Name", p.Name);
        //        cmd.Parameters.AddWithValue("@Age", p.Age);
        //        cmd.Parameters.AddWithValue("@Id", p.Id);
        //        cmd.ExecuteNonQuery();
        //    }
        //}



    }
}

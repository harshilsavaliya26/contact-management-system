using Microsoft.Data.SqlClient;
using System.Data;

namespace cn.Models
{
    public class usermodel
    {
            public int id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string password { get; set; }
            public string contactnumber { get; set; }
            public string gender { get; set; }
            public string city { get; set; }
            public string address { get; set; }

            SqlConnection con = new SqlConnection(
                "Data Source=.\\SQLEXPRESS;Initial Catalog=contact-book;Integrated Security=True;TrustServerCertificate=True");

            // REGISTER
            public int InsertData(
                string name,
                string email,
                string password,
                string contactnumber,
                string gender,
                string city,
                string address)
            {
                SqlCommand cmd = new SqlCommand(
                    "insert into [dbo].[user] " +
                    "(name,email,password,contactnumber,gender,city,address) " +
                    "values('" + name + "','" + email + "','" + password + "','" +
                    contactnumber + "','" + gender + "','" + city + "','" + address + "')",
                    con);

                con.Open();

                int result = cmd.ExecuteNonQuery();

                con.Close();

                return result;
            }

            // LOGIN
            public DataSet Login(string email, string password)
            {
                SqlCommand cmd = new SqlCommand(
                    "select * from [dbo].[user] where email='" +
                    email + "' and password='" + password + "'",
                    con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                da.Fill(ds);

                return ds;
            }

            // SINGLE USER
            public DataSet GetUserById(int id)
            {
                SqlCommand cmd = new SqlCommand(
                    "select * from [dbo].[user] where id='" + id + "'",
                    con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                da.Fill(ds);

                return ds;
            }

            // ALL USERS (optional)
            public DataSet GetData()
            {
                SqlCommand cmd = new SqlCommand(
                    "select * from [dbo].[user]",
                    con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                da.Fill(ds);

                return ds;
            }

        // UPDATE ACCOUNT
        public int UpdateData(
            string name,
            string email,
            string password,
            string contactnumber,
            string gender,
            string city,
            string address,
            int id)
        {
            SqlCommand cmd = new SqlCommand(
                "update [dbo].[user] set " +
                "name='" + name + "'," +
                "email='" + email + "'," +
                "password='" + password + "'," +
                "contactnumber='" + contactnumber + "'," +
                "gender='" + gender + "'," +
                "city='" + city + "'," +
                "address='" + address + "'" +
                " where id='" + id + "'",
                con);

            con.Open();

            int result = cmd.ExecuteNonQuery();

            con.Close();

            return result;
        }
    }
}

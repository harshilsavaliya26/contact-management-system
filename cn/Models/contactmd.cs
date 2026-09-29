
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace cn.Models
{
    public class contactmd
    {
        public int id { get; set; }
        public int userid { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string contactnumber { get; set; }
        public string address { get; set; }

        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=contact-book;Integrated Security=True;TrustServerCertificate=True");

        public int InsertData(
        int userid,
        string name,
        string email,
        string contactnumber,
        string address)
        {
            SqlCommand cmd = new SqlCommand("insert into [dbo].[contacts](userid,name,email,contactnumber,address) values('" +
            userid + "','" +
            name + "','" +
            email + "','" +
            contactnumber + "','" +
            address + "')", con);

            con.Open();

            int result = cmd.ExecuteNonQuery();

            con.Close();

            return result;
        }

        public DataSet GetData()
        {
            SqlCommand cmd =
            new SqlCommand("select * from [dbo].[contacts]", con);

            SqlDataAdapter da =
            new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            return ds;
        }

        public int Delete(int id)
        {
            SqlCommand cmd =
            new SqlCommand("delete from [dbo].[contacts] where id='" + id + "'", con);

            con.Open();

            int result = cmd.ExecuteNonQuery();

            con.Close();

            return result;
        }

        public DataSet GetSingleData(int id)
        {
            SqlCommand cmd =
            new SqlCommand("select * from [dbo].[contacts] where id='" + id + "'", con);

            SqlDataAdapter da =
            new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            return ds;
        }

        public int Update(
            string name,
            string email,
            string contactnumber,
            string address,
            int id)
        {
            SqlCommand cmd = new SqlCommand(
            "update contacts set " +
            "name='" + name + "'," +
            "email='" + email + "'," +
            "contactnumber='" + contactnumber + "'," +
            "address='" + address + "'" +
            " where id='" + id + "'", con);

            con.Open();

            int result = cmd.ExecuteNonQuery();

            con.Close();

            return result;
        }

        // add contact 
        public DataSet GetContactByUserId(int userid)
        {
            SqlCommand cmd = new SqlCommand(
            "select * from [dbo].[contacts] where userid='" + userid + "'", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            return ds;
        }

        //total contact count
        public int TotalContact(int userid)
        {
            SqlCommand cmd = new SqlCommand(
                "select count(*) from contacts where userid='" + userid + "'",
                con);

            con.Open();

            int total = (int)cmd.ExecuteScalar();

            con.Close();

            return total;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppSec_Assignment_191473X
{
    public partial class Detail : System.Web.UI.Page
    {
        string AppSecDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AppSecDB"].ConnectionString;
        string firstName = null;
        string lastName = null;
        string email = null;
        string dob = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Email"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
            {
                if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
                {
                    Response.Redirect("Login.aspx", false);
                }
                else
                {
                    email = (string)Session["email"];

                    displayUserProfile(email);
                }
                
            }
            else
            {
                Response.Redirect("LoginPage.aspx", false);
            }
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            Response.Redirect("LoginPage.aspx", false);

            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            }
            if (Request.Cookies["AuthToken"] != null)
            {
                Response.Cookies["AuthToken"].Value = string.Empty;
                Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
            }
        }

        protected void displayUserProfile (string email)
        {
            SqlConnection connection = new SqlConnection(AppSecDBConnectionString);
            string sql = "Select * From Account Where  Email=@email";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@email", email);

            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["FirstName"] != DBNull.Value)
                        {
                            lblFirstName.Text = reader["FirstName"].ToString();
                        }

                        if (reader["LastName"] != DBNull.Value)
                        {
                            lblLastName.Text = reader["LastName"].ToString();
                        }

                        if (reader["Email"] != DBNull.Value)
                        {
                            lblEmail.Text = reader["Email"].ToString();
                        }

                        if (reader["DOB"] != DBNull.Value)
                        {
                            lblDOB.Text = reader["DOB"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
        }

    }
}
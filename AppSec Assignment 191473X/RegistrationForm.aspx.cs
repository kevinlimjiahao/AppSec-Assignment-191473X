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
    public partial class RegistrationForm : System.Web.UI.Page
    {
        string AppSecDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AppSecDB"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int scores = checkPassword(tbPassword.Text);
            string status = "";
            switch (scores)
            {
                case 1:
                    status = "Very Weak";
                    break;

                case 2:
                    status = "Weak";
                    break;

                case 3:
                    status = "Medium";
                    break;

                case 4:
                    status = "Strong";
                    break;

                case 5:
                    status = "Very Strong";
                    break;

                default:
                    break;
            }
            lblChkPwd.Text = "Status:" + status;
            if (scores < 4)
            {
                lblChkPwd.ForeColor = Color.Red;
                return;
            }
            lblChkPwd.ForeColor = Color.Green;
            if (status == "Very Strong" || status == "Strong")
            {
                string pwd = tbPassword.Text.ToString().Trim(); ;

                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                byte[] saltByte = new byte[8];

                rng.GetBytes(saltByte);
                salt = Convert.ToBase64String(saltByte);

                SHA512Managed hashing = new SHA512Managed();

                string pwdWithSalt = pwd + salt;
                byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwd));
                byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));

                finalHash = Convert.ToBase64String(hashWithSalt);

                RijndaelManaged cipher = new RijndaelManaged();
                cipher.GenerateKey();
                Key = cipher.Key;
                IV = cipher.IV;
                int emailscore = checkEmail(tbEmail.Text);
                if (emailscore > 0)
                {
                    lblEmailChk.Text = "Email is already in used!";
                    lblEmailChk.ForeColor = Color.Red;
                }
                else
                {
                    lblEmailChk.Text = "";
                    createAccount();
                    lblSuccess.Text = "Account has been created!";
                    lblSuccess.ForeColor = Color.Blue;
                    tbFirstName.Text = "";
                    tbLastName.Text = "";
                    tbCreditCard.Text = "";
                    tbEmail.Text = "";
                    tbDOB.Text = "";
                }
            }
        }

        private int checkPassword(string password)
        {
            int score = 0;

            if (password.Length < 8)
            {
                return 1;
            }
            else
            {
                score = 1;
            }
            if (Regex.IsMatch(password, "[a-z]"))
            {
                score++;
            }
            if (Regex.IsMatch(password, "[A-Z]"))
            {
                score++;
            }
            if (Regex.IsMatch(password, "[0-9]"))
            {
                score++;
            }
            if (Regex.IsMatch(password, "[^A-Za-z0-9]"))
            {
                score++;
            }

            return score;
        }

        public void createAccount()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(AppSecDBConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Account VALUES(@FirstName,@LastName,@CreditCardNo,@Email,@PasswordHash,@PasswordSalt,@DOB,@IV,@Key)"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@FirstName", HttpUtility.HtmlEncode(tbFirstName.Text.Trim()));
                            cmd.Parameters.AddWithValue("@LastName", HttpUtility.HtmlEncode(tbLastName.Text.Trim()));
                            cmd.Parameters.AddWithValue("@CreditCardNo", Convert.ToBase64String(encryptData(tbCreditCard.Text.Trim())));
                            cmd.Parameters.AddWithValue("@Email", HttpUtility.HtmlEncode(tbEmail.Text.Trim()));
                            cmd.Parameters.AddWithValue("@PasswordHash", finalHash);
                            cmd.Parameters.AddWithValue("@PasswordSalt", salt);
                            cmd.Parameters.AddWithValue("@DOB", tbDOB.Text.Trim());
                            cmd.Parameters.AddWithValue("@IV", Convert.ToBase64String(IV));
                            cmd.Parameters.AddWithValue("@Key", Convert.ToBase64String(Key));
                            cmd.Connection = con;

                            try
                            {
                                con.Open();
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                lblSuccess.Text = "An error has occured";
                            }
                            finally
                            {
                                con.Close();
                            }
                            //con.Open();
                            //cmd.ExecuteNonQuery();
                            //con.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        protected byte [] encryptData(string data)
        {
            byte[] cipherText = null;
            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;
                ICryptoTransform encryptTransform = cipher.CreateEncryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(data);
                cipherText = encryptTransform.TransformFinalBlock(plainText, 0, plainText.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }
            return cipherText;
        }

        private int checkEmail(string email)
        {
            int score = 0;

            SqlConnection con = new SqlConnection(AppSecDBConnectionString);
            string sqlStmt = "Select Email From Account Where Email='" + HttpUtility.HtmlEncode(tbEmail.Text) + "'";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count != 0)
            {
                score++;
            }
            return score;
        }
   
    }
}
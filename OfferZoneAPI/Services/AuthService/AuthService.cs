using MySql.Data.MySqlClient;
using OfferZoneAPI.Common;
using OfferZoneAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfferZoneAPI.Services.AuthService
{
    public class AuthService
    {
        private readonly DbContext _context;

        public AuthService(DbContext context)
        {
            _context = context;
            _context.GetConnection();
        }

        public LoginModel AdminLogin(LoginModel login)
        {
            MySqlConnection connection = _context.GetConnection();
            LoginModel adminIsLogin = new LoginModel();
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "SELECT usertype FROM login WHERE username= '"+login.UserName+"' AND password= '"+login.Password+"'";
                cmd.Prepare();
                //using (var reader = cmd.ExecuteScalar())
                //{
                //    //while (reader.Read())
                //    //{
                //    //    admins.Add(new AdminRegistration()
                //    //    {
                //    //        AdminId = Convert.ToInt32(reader["aid"]),
                //    //        AdminName = reader["aname"].ToString(),
                //    //        AdminEmail = reader["aemail"].ToString(),
                //    //        AdminMobile = Convert.ToInt32(reader["amobile"]),
                //    //        AdminDob = reader["adob"].ToString()
                //    //    });
                //    //}
                //}
                adminIsLogin.UserType = Convert.ToString(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
            return adminIsLogin;
        }
    }
}

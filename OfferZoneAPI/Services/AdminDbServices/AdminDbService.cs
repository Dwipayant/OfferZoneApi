using MySql.Data.MySqlClient;
using OfferZoneAPI.Common;
using OfferZoneAPI.Models;
using System;
using System.Collections.Generic;

namespace OfferZoneAPI.Services.AdminDbServices
{
    public class AdminDbService
    {
        private readonly DbContext _context;
        public AdminDbService(DbContext context)
        {
            _context = context;
            _context.GetConnection();
        }

        public string CreateAdmin(AdminRegistration adminData)
        {
            MySqlConnection connection = _context.GetConnection();
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "insert into adminregistration (aname,aemail,adob,amobile) VALUES (@aname, @aemail, @adob, @amobile)";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@aname", adminData.AdminName);
                cmd.Parameters.AddWithValue("@aemail", adminData.AdminEmail);
                cmd.Parameters.AddWithValue("@adob", adminData.AdminDob);
                cmd.Parameters.AddWithValue("@amobile", adminData.AdminMobile);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
            return "Created";
        }

        public List<AdminRegistration> GetAdmins()
        {
            MySqlConnection connection = _context.GetConnection();
            List<AdminRegistration> admins = new List<AdminRegistration>();
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "SELECT * FROM adminregistration";
                cmd.Prepare();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        admins.Add(new AdminRegistration()
                        {
                            AdminId = Convert.ToInt32(reader["aid"]),
                            AdminName = reader["aname"].ToString(),
                            AdminEmail = reader["aemail"].ToString(),
                            AdminMobile = Convert.ToInt32(reader["amobile"]),
                            AdminDob = reader["adob"].ToString()
                        });
                    }
                }
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
            return admins;
        }
    }
}

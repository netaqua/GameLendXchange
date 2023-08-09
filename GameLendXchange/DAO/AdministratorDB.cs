using GameLendXchange.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLendXchange.DAO
{
    internal class AdministratorDB
    {

        private String connectionString;
        public AdministratorDB() 
        {
            connectionString = ConfigurationManager.ConnectionStrings["XchangeDB"].ConnectionString;
        }

        public Administrator GetConnection(String Username, String passWord)
        {
            Administrator a = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.Administrator WHERE username=@Username and password=@Password", connection);

                cmd.Parameters.AddWithValue("Username", Username);
                cmd.Parameters.AddWithValue("Password", passWord);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        a = new Administrator();
                        a.IdUser = reader.GetInt32("idAdmin");
                        a.Username = reader.GetString("username");
                        a.Password = reader.GetString("password");
                    }
                }
            }
            return a;
        }
    }
}

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
    internal class CopyDB
    {
        private String connectionString;

        public CopyDB()
        {
            connectionString = ConfigurationManager.ConnectionStrings["XchangeDB"].ConnectionString;
        }
        public bool Create(Copy c)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString)) // Pas oublier l'auto_incrementation dans la base de donnée pour l'ID
            {
                SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.Copy(idCopy, idVideoGame, idOwner) VALUES({c.IdCopy}, {c.VideoGame.IdGame}, {c.Owner.IdUser})", connection);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }
            return success;
        }
        public Copy Read(int idGame, int idOwner)
        {
            Copy c = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.Copy WHERE idGame='@IdVideoGame' and idOwner='@IdOwner'", connection);

                cmd.Parameters.AddWithValue("IdVideoGame", idGame);
                cmd.Parameters.AddWithValue("IdOwner", idOwner);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        c = new Copy();
                        c.IdCopy = reader.GetInt32("idCopy");
                        c.Owner.IdUser = reader.GetInt32("idOwner");
                        c.VideoGame.IdGame = reader.GetInt32("idVideoGame");
                    }
                }
            }
            return c;
        }

        public bool Udpdate(Copy c)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE dbo.copy SET idVideoGame = {c.VideoGame.IdGame}, idOwner = {c.Owner.IdUser} WHERE idCopy = {c.IdCopy}", connection);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }
            return success;
        }


        public bool Delete(Copy c)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"DELETE FROM dbo.copy WHERE idCopy={c.IdCopy}", connection);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }
            return success;
        }

        public List<Copy> ReadAll()
        {
            List<Copy> copys = new List<Copy>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Copy", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Copy c = new Copy();
                        c.IdCopy = reader.GetInt32("idCopy");
                        c.Owner.IdUser = reader.GetInt32("idOwner");
                        c.VideoGame.IdGame = reader.GetInt32("idVideoGame");
                        copys.Add(c);
                    }
                }
            }
            return copys;
        }
    }
}

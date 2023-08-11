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
            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.Copy(idVideoGame, idOwner) VALUES({c.VideoGame.IdGame}, {c.Owner.IdUser})", connection);
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
                SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.Copy WHERE idVideoGame=@IdVideoGame and idOwner=@IdOwner", connection);

                cmd.Parameters.AddWithValue("IdVideoGame", idGame);
                cmd.Parameters.AddWithValue("IdOwner", idOwner);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        c = new Copy();
                        c.IdCopy = reader.GetInt32("idCopy");
                        c.Owner = Player.GetPlayerById(Convert.ToInt32(reader["IdOwner"]));
                        c.VideoGame = VideoGame.ReadId(idGame);
                        c.Available = reader.GetBoolean("isAvailable");
                    }
                }
            }
            return c;
        }

        public bool Update(Copy c)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                int Ava = 0;
                if(c.Available== true)
                {
                    Ava = 1;
                }
                else
                {
                    Ava = 0;
                }
                SqlCommand cmd = new SqlCommand($"UPDATE dbo.copy SET idVideoGame = {c.VideoGame.IdGame}, idOwner = {c.Owner.IdUser}, isAvailable = {Ava} WHERE idCopy = {c.IdCopy}", connection);
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
                        c.Available = reader.GetBoolean("isAvailable");
                        copys.Add(c);
                    }
                }
            }
            return copys;
        }

        // Méthode pour récupérer toutes les copies d'un jeu à partir de son identifiant
        public List<Copy> ReadAllCopiesByVideoGame(int idGame)
        {
            List<Copy> copies = new List<Copy>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT IdCopy, IdOwner, isAvailable FROM Copy WHERE IdVideoGame = @IdVideoGame";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdVideoGame", idGame);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Copy copy = new Copy
                                {
                                    IdCopy = Convert.ToInt32(reader["IdCopy"]),
                                    Owner = Player.GetPlayerById(Convert.ToInt32(reader["IdOwner"])),
                                    Available = reader.GetBoolean("isAvailable"),
                                    VideoGame = VideoGame.ReadId(idGame)
                                };

                                copies.Add(copy);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la lecture des copies : " + ex.Message);
            }

            return copies;
        }

        public void ReleaseCopy(int cId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE dbo.copy SET isAvailable = 1 WHERE idCopy = {cId}", connection);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
            }
        }

        public void Borrow(Copy c)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE dbo.copy SET isAvailable = 0 WHERE idCopy = {c.IdCopy}", connection);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
            }
        }
    }
}

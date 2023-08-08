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
    internal class VideoGameDB
    {
        private String connectionString;

        public VideoGameDB()
        {
            connectionString = ConfigurationManager.ConnectionStrings["XchangeDB"].ConnectionString;
        }

        public bool Create(VideoGame v)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.VideoGame(Name, CreditCost, Console) VALUES('{v.Name}', '{v.CreditCost}', '{v.Console}')", connection);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }
            return success;
        }

        public VideoGame Read(String videoGameName)
        {
            VideoGame v = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.VideoGame WHERE name='@name'", connection);

                cmd.Parameters.AddWithValue("name", videoGameName);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        v = new VideoGame();
                        v.IdGame = reader.GetInt32("idVideoGame");
                        v.Name = reader.GetString("name");
                        v.CreditCost = reader.GetInt32("creditCost");
                        v.Console = reader.GetString("Console");
                    }
                }
            }
            return v;
        }

        public VideoGame ReadId(int idVideoGame)
        {
            VideoGame v = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.VideoGame WHERE idVideoGame=@idVideoGame", connection);

                cmd.Parameters.AddWithValue("idVideoGame", idVideoGame);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        v = new VideoGame();
                        v.IdGame = reader.GetInt32("idVideoGame");
                        v.Name = reader.GetString("name");
                        v.CreditCost = reader.GetInt32("creditCost");
                        v.Console = reader.GetString("Console");
                    }
                }
            }
            return v;
        }

        public bool Update(VideoGame v)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE dbo.VideoGame SET Name = '{v.Name}', CreditCost = '{v.CreditCost}', Console = '{v.Console}' WHERE idVideoGame = {v.IdGame}", connection);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }
            return success;
        }

        public bool Delete(VideoGame v)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"DELETE FROM dbo.VideoGame WHERE idVideoGame = {v.IdGame}", connection);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }
            return success;
        }

        public List<VideoGame> ReadAll()
        {
            List<VideoGame> videoGames = new List<VideoGame>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.VideoGame", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VideoGame videoGame = new VideoGame();
                        videoGame.IdGame = reader.GetInt32("idVideoGame");
                        videoGame.Name = reader.GetString("name");
                        videoGame.CreditCost = reader.GetInt32("creditCost");
                        videoGame.Console = reader.GetString("Console");
                        videoGames.Add(videoGame);
                    }
                }
            }
            return videoGames;
        }

        /**************************************************** SORT BY CONSOLES *****************************************************/

        public List<VideoGame> SortByPlaystation()
        {
            List<VideoGame> videoGames = new List<VideoGame>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.VideoGame where console in ('PS5','PS4','PS3')", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VideoGame videoGame = new VideoGame();
                        videoGame.IdGame = reader.GetInt32("idVideoGame");
                        videoGame.Name = reader.GetString("name");
                        videoGame.CreditCost = reader.GetInt32("creditCost");
                        videoGame.Console = reader.GetString("Console");
                        videoGames.Add(videoGame);
                    }
                }
            }
            return videoGames;
        }

        public List<VideoGame> SortByXbox()
        {
            List<VideoGame> videoGames = new List<VideoGame>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.VideoGame where console in ('XBOX ONE','XBOX 360')", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VideoGame videoGame = new VideoGame();
                        videoGame.IdGame = reader.GetInt32("idVideoGame");
                        videoGame.Name = reader.GetString("name");
                        videoGame.CreditCost = reader.GetInt32("creditCost");
                        videoGame.Console = reader.GetString("Console");
                        videoGames.Add(videoGame);
                    }
                }
            }
            return videoGames;
        }

        public List<VideoGame> SortByNintendo()
        {
            List<VideoGame> videoGames = new List<VideoGame>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.VideoGame where console in ('SWITCH')", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VideoGame videoGame = new VideoGame();
                        videoGame.IdGame = reader.GetInt32("idVideoGame");
                        videoGame.Name = reader.GetString("name");
                        videoGame.CreditCost = reader.GetInt32("creditCost");
                        videoGame.Console = reader.GetString("Console");
                        videoGames.Add(videoGame);
                    }
                }
            }
            return videoGames;
        }



        public VideoGame ReadById(int idGame)
        {
            VideoGame videoGame = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT IdVideoGame, Name, CreditCost, Console FROM VideoGame WHERE IdVideoGame = @IdVideoGame";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdVideoGame", idGame);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Créer un nouvel objet VideoGame avec les données de la base de données
                                videoGame = new VideoGame
                                {
                                    IdGame = Convert.ToInt32(reader["IdVideoGame"]),
                                    Name = Convert.ToString(reader["Name"]),
                                    CreditCost = Convert.ToInt32(reader["CreditCost"]),
                                    Console = Convert.ToString(reader["Console"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Gérer les erreurs de lecture de la base de données
                Console.WriteLine("Erreur lors de la lecture du jeu : " + ex.Message);
            }

            return videoGame;
        }

        public bool UpdateCreditCost(VideoGame videoGame)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("UPDATE dbo.VideoGame SET creditCost = @credit WHERE idVideoGame = @id", connection);
                cmd.Parameters.AddWithValue("id", videoGame.IdGame);
                cmd.Parameters.AddWithValue("credit", videoGame.CreditCost);
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }
            return success;
        }


    }
}

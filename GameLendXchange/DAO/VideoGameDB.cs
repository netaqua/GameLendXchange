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
                        v.IdGame = reader.GetInt32("idGame");
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
                SqlCommand cmd = new SqlCommand($"UPDATE dbo.VideoGame SET Name = '{v.Name}', CreditCost = '{v.CreditCost}', Console = '{v.Console}' WHERE idGame = {v.IdGame}", connection);
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
                SqlCommand cmd = new SqlCommand($"DELETE FROM dbo.VideoGame WHERE idGame = {v.IdGame}", connection);
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
                        videoGame.IdGame = reader.GetInt32("idGame");
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
                        videoGame.IdGame = reader.GetInt32("idGame");
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
                        videoGame.IdGame = reader.GetInt32("idGame");
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
                        videoGame.IdGame = reader.GetInt32("idGame");
                        videoGame.Name = reader.GetString("name");
                        videoGame.CreditCost = reader.GetInt32("creditCost");
                        videoGame.Console = reader.GetString("Console");
                        videoGames.Add(videoGame);
                    }
                }
            }
            return videoGames;
        }
    }
}

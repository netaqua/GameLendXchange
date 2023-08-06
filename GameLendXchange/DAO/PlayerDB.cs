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
    internal class PlayerDB
    {
        private String connectionString;

        public PlayerDB()
        {
            connectionString = ConfigurationManager.ConnectionStrings["XchangeDB"].ConnectionString;
        }

        public Player GetConnection(String playerUsername, String passWord)
        {
            Player p = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.player WHERE username=@Username and password=@Password", connection);

                cmd.Parameters.AddWithValue("Username", playerUsername);
                cmd.Parameters.AddWithValue("Password", passWord);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        p = new Player();
                        p.IdUser = reader.GetInt32("idPlayer");
                        p.Credit = reader.GetInt32("credit");
                        p.Pseudo = reader.GetString("pseudo");
                        p.Username = reader.GetString("username");
                        p.Password = reader.GetString("password");//Il Faut l'ajouter a player pour pouvoir verifier que la connection peut se faire
                        p.RegistrationDate = reader.GetDateTime("registrationDate");
                        p.DateOfBirth = reader.GetDateTime("dateOfBirth");
                    }
                }
            }
            return p;//Ne pas oublier de verifier si les information sont entré pour une connection, sinon accè bloqué.
        }

        public bool Create(Player p)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand($"INSERT into dbo.Player(username,password,dateOfBirth, registrationDate,pseudo) values ('{p.Username}','{p.Password}','{p.RegistrationDate:yyyy-MM-dd}', '{p.DateOfBirth:yyyy-MM-dd}','{p.Pseudo}')", connection); 
                    connection.Open(); // permet d'exécuter une commande d'insert / update / delete
                    int res = cmd.ExecuteNonQuery();
                    success = res > 0;
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627) // Numéro d'erreur spécifique pour la violation de contrainte d'unicité
                    {
                        // Gérer ici le cas où le nom d'utilisateur est déjà présent
                        // par exemple, afficher un message d'erreur ou effectuer une autre action appropriée
                        Console.WriteLine("Le nom d'utilisateur ou le pseudo est déjà utilisé.");
                    }
                    else
                    {
                        Console.WriteLine("Une erreur SQL s'est produite : " + ex.Message);
                    }
                }
            }

            return success;
        }

        public Player Read(String playerUsername)
        {
            Player ?p = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.player WHERE username='@Username'", connection);

                cmd.Parameters.AddWithValue("Username", playerUsername);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        p = new Player();
                        p.IdUser = reader.GetInt32("idPlayer");
                        p.Credit = reader.GetInt32("credit");
                        p.Username = reader.GetString("username");
                        p.Pseudo = reader.GetString("pseudo");
                        p.Password = reader.GetString("password");//Il Faut l'ajouter a player pour pouvoir verifier que la connection peut se faire
                        p.RegistrationDate = reader.GetDateTime("registrationDate");
                        p.DateOfBirth = reader.GetDateTime("dateOfBirth");
                    }
                }
            }
            return p;
        }

        public bool Update(Player p)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE dbo.player SET credit = {p.Credit}, username = '{p.Username}', password = '{p.Password}', registrationDate = {p.RegistrationDate}, dateOfBirth = {p.DateOfBirth} WHERE idPlayer = {p.IdUser}", connection);
                connection.Open();
                success = cmd.ExecuteNonQuery() > 0;
            }
            return success;
        }

        public bool Delete(Player p)
        {
            bool success = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"DELETE FROM dbo.player WHERE idPlayer=@id", connection);
                    cmd.Parameters.AddWithValue("id", p.IdUser);
                    connection.Open();
                    success = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException e)
            {
                throw new Exception("Une erreur sql s'est produite! " + e.ErrorCode);
            }

            return success;
        }


        public List<Player> ReadAll()
        {
            List<Player> players = new List<Player>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Player", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Player player = new Player();
                        player.IdUser = reader.GetInt32("idPlayer");
                        player.Credit = reader.GetInt32("credit");
                        player.Pseudo = reader.GetString("pseudo");
                        player.Username = reader.GetString("username");
                        player.Password = reader.GetString("password");//Il Faut l'ajouter a player pour pouvoir verifier que la connection peut se faire
                        player.RegistrationDate = reader.GetDateTime("registrationDate");
                        player.DateOfBirth = reader.GetDateTime("dateOfBirth");
                        players.Add(player);
                    }
                }
            }
            return players;
        }
        public Player ReadPlayer(String playerUsername, String playerPassword)       //modifier pour n'avoir que les infos de son Player
         {
             Player p = new Player();
             using (SqlConnection connection = new SqlConnection(connectionString))
             {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Player WHERE username='@Username' and password= '@Password", connection);
                connection.Open();
                cmd.Parameters.AddWithValue("Username", playerUsername);
                cmd.Parameters.AddWithValue("Password", playerPassword);
                using (SqlDataReader reader = cmd.ExecuteReader())
                 {
                    if (reader.Read())
                    {
                        p = new Player();
                        p.IdUser = reader.GetInt32("idPlayer");
                        p.Credit = reader.GetInt32("credit");
                        p.Username = reader.GetString("username");
                        p.Pseudo = reader.GetString("pseudo");
                        p.Password = reader.GetString("password");//Il Faut l'ajouter a player pour pouvoir verifier que la connection peut se faire
                        p.RegistrationDate = reader.GetDateTime("registrationDate");
                        p.DateOfBirth = reader.GetDateTime("dateOfBirth");
                    }
                }
             }
             return p;
         }


        /*public bool Insert(Player p)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"INSERT into dbo.Player(username,password,dateOfBirth, registrationDate,pseudo, credit) values ('{p.Username}','{p.Password}','{p.DateOfBirth}','{p.RegistrationDate}','{p.Pseudo}','{p.Credit}')", connection);
                connection.Open(); // permet d'excécuter une commande d'insert / update / delete
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }

            return success;
        }*/

        //Test d'un autre type de insert
        public bool Insert(Player p)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand($"INSERT into dbo.Player(username,password,dateOfBirth, registrationDate,pseudo) values ('{p.Username}','{p.Password}','{p.DateOfBirth}','{p.RegistrationDate}','{p.Pseudo}')", connection);
                    connection.Open(); // permet d'exécuter une commande d'insert / update / delete
                    int res = cmd.ExecuteNonQuery();
                    success = res > 0;
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627) // Numéro d'erreur spécifique pour la violation de contrainte d'unicité
                    {
                        // Gérer ici le cas où le nom d'utilisateur est déjà présent
                        // par exemple, afficher un message d'erreur ou effectuer une autre action appropriée
                        Console.WriteLine("Le nom d'utilisateur ou le pseudo est déjà utilisé.");
                    }
                    else
                    {
                        Console.WriteLine("Une erreur SQL s'est produite : " + ex.Message);
                    }
                }
            }

            return success;
        }

        public Player GetPlayerById(int idPlayer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM dbo.Player WHERE idPlayer = @idPlayer";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@idPlayer", idPlayer);
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Player player = new Player
                            {
                                IdUser = reader.GetInt32("idPlayer"),
                                Credit = reader.GetInt32("credit"),
                                Username = reader.GetString("username"),
                                Pseudo = reader.GetString("pseudo"),
                                Password = reader.GetString("password"),
                                RegistrationDate = reader.GetDateTime("registrationDate"),
                                DateOfBirth = reader.GetDateTime("dateOfBirth"),
                        };
                            return player;
                        }
                    }
                }
            }

            return null; // Return null if player not found or an error occurred.
        }

        public static void AddBirthdayBonus(Player player)
        {
            if (player.DateOfBirth.Month == DateTime.Now.Month && player.DateOfBirth.Day == DateTime.Now.Day)
            {
                // Vérifier si le bonus d'anniversaire n'a pas encore été reçu (non fonctionnel pour le moment)
                if (!player.IsBirthdayBonusReceived)
                {
                    player.Credit += 2;
                    player.IsBirthdayBonusReceived = true; // Mettre à jour le champ pour indiquer que le bonus a été reçu

                    PlayerDB playerDB = new PlayerDB();

                    using (SqlConnection connection = new SqlConnection(playerDB.connectionString))
                    {
                        connection.Open();

                        string sql = $"UPDATE dbo.Player SET credit = @credit WHERE idPlayer = @idPlayer";
                        SqlCommand cmd = new SqlCommand(sql, connection);
                        cmd.Parameters.AddWithValue("@credit", player.Credit);
                        cmd.Parameters.AddWithValue("@isReceived", player.IsBirthdayBonusReceived);
                        cmd.Parameters.AddWithValue("@idPlayer", player.IdUser);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Joyeux anniversaire !");
                        }
                        else
                        {
                            Console.WriteLine("Erreur");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Vous avez déjà reçu votre bonus d'anniversaire ...");
                }
            }
        }

    }
}

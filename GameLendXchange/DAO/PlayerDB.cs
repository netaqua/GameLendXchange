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
                    SqlCommand cmd = new SqlCommand($"INSERT into dbo.Player(username,password,dateOfBirth, registrationDate,pseudo, credit) values ('{p.Username}','{p.Password}','{p.RegistrationDate:yyyy-MM-dd}', '{p.DateOfBirth:yyyy-MM-dd}','{p.Pseudo}',0)", connection); 
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
                    SqlCommand cmd = new SqlCommand($"INSERT into dbo.Player(username,password,dateOfBirth, registrationDate,pseudo, credit) values ('{p.Username}','{p.Password}','{p.DateOfBirth}','{p.RegistrationDate}','{p.Pseudo}','{p.Credit}')", connection);
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


    }
}

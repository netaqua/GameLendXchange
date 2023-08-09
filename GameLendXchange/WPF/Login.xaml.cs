using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GameLendXchange;
using GameLendXchange.Classes;

namespace GameLendXchange.WPF
{
    /// <summary>
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        private String connectionString;
        public Login()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["XchangeDB"].ConnectionString;
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new MainWindow());
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();
        }

        //-----------------------------------------------------//
        //------------------ LOGIN PLAYER --------------------//
        //---------------------------------------------------//




        
        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Password;

            User user = new User();
            User loggedInUser = user.Login(username, password);

            if (loggedInUser is Administrator admin)
            {
                AccueilAdmin accueilAdminPage = new AccueilAdmin(admin);
                NavigationService.Navigate(accueilAdminPage);
            }
            else if (loggedInUser is Player player)
            {
                AccueilPlayer accueilPlayerPage = new AccueilPlayer(player);
                NavigationService.Navigate(accueilPlayerPage);
            }
            else
            {
                
                MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.");
            }
        }



        /*

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {

            User user = new User();
            user.Username = usernameBox.Text;
            user.Password = passwordBox.Text;

            user = user.Login(user.Username, user.Password);
            bool adminé = false;
            bool player = false;

            if (user is Administrator)
            {
                AccueilAdmin accueilAdminPage = new AccueilAdmin(user);
                NavigationService.Navigate(accueilAdminPage);
            }
            else if (user is Player)
            {
                player = true;
            }
            else
            {
                MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect !");
            }


            switch (obj = user.Login(user.Username, user.Password))
            {
                case obj is Administrator:
                    
                break;
                case obj is Player:
                    Player player = obj;
                AccueilPlayer accueilPlayerPage = new AccueilPlayer(player);
                NavigationService.Navigate(accueilPlayerPage);
                break;

                default :
                    MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect !");
                break;

            }

        }


        // Vérifier les informations d'identification dans la base de données
        bool isValid = VerifyCredentials(username, password);

        if (isValid)
        {
            // Récupérer le joueur à partir de la base de données
            Player player = GetPlayerFromDatabase(username);

            if (player != null)
            {
                // Passer le joueur à la page d'accueil du joueur
                AccueilPlayer accueilPlayerPage = new AccueilPlayer(player);
                NavigationService.Navigate(accueilPlayerPage);
            }
            else
            {
                MessageBox.Show("Player introuvable !");
            }
        }
        else
        {
            MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect !");
        }
    }

    private bool VerifyCredentials(string username, string password)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT COUNT(*) FROM dbo.Player WHERE Username = @Username AND Password = @Password";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);

            connection.Open();
            int count = (int)command.ExecuteScalar();
            return count > 0;
        }
    }

    private Player GetPlayerFromDatabase(string username)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM dbo.Player WHERE Username = @Username";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", username);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                Player player = new Player
                {
                    IdUser = (int)reader["IdPlayer"],
                    Username = (string)reader["Username"],
                    Password = (string)reader["Password"],
                    Pseudo = (string)reader["Pseudo"],
                    Credit = (int)reader["Credit"],
                    RegistrationDate = (DateTime)reader["RegistrationDate"],
                    DateOfBirth = (DateTime)reader["DateOfBirth"],
            };

                return player;
            }

            return null;
        }
    }

    //-----------------------------------------------------//
    //------------------ LOGIN ADMIN ---------------------//
    //---------------------------------------------------//

    private void LoginBtnAdmin_Click(object sender, RoutedEventArgs e)
    {
        string username = adminUsernameBox.Text;
        string password = adminPasswordBox.Text;

        // Vérifier les informations d'identification dans la base de données
        bool isValid = VerifyCredentialsAdmin(username, password);

        if (isValid)
        {
            // Récupérer l'admin à partir de la base de données
            Administrator admin = GetAdminFromDatabase(username);

            if (admin != null)
            {
                // Passer l'admin à la page d'accueil des admin
                AccueilAdmin accueilAdminPage = new AccueilAdmin(admin);
                NavigationService.Navigate(accueilAdminPage);
            }
            else
            {
                MessageBox.Show("Admin introuvable !");
            }
        }
        else
        {
            MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect !");
        }
    }

    private bool VerifyCredentialsAdmin(string username, string password)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT COUNT(*) FROM dbo.Administrator WHERE Username = @Username AND Password = @Password";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);

            connection.Open();
            int count = (int)command.ExecuteScalar();
            return count > 0;
        }
    }

    private Administrator GetAdminFromDatabase(string username)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM dbo.Administrator WHERE Username = @Username";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", username);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                Administrator admin = new Administrator
                {
                    IdUser = (int)reader["IdAdmin"],
                    Username = (string)reader["Username"],
                    Password = (string)reader["Password"],
                };

                return admin;
            }

            return null;
        }
    }
        */
    }
}

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
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();
        }

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
    }
}

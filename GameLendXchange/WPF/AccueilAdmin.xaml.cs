using GameLendXchange.Classes;
using System;
using System.Collections.Generic;
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

namespace GameLendXchange.WPF
{
    /// <summary>
    /// Logique d'interaction pour AccueilAdmin.xaml
    /// </summary>
    public partial class AccueilAdmin : Page
    {
        public AccueilAdmin(Administrator a)
        {
            InitializeComponent();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string buttonContent = button.Content.ToString();

            switch (buttonContent)
            {
                case "Video Games":
                    List<VideoGame> videoGames = VideoGame.GetGames();
                    GridAdmin.ItemsSource = videoGames;
                    break;
                case "Player":
                    List<Player> players = Player.GetPlayers();
                    GridAdmin.ItemsSource = players;
                    break;
                case "Reservations":
                    List<Booking> bookings = Booking.GetBookings();
                    GridAdmin.ItemsSource = bookings;
                    break;
                case "Locations":
                    List<Loan> loans = Loan.GetLoans();
                    GridAdmin.ItemsSource = loans;
                    break;
                default:
                    GridAdmin.ItemsSource = null;
                    break;
            }
        }
    }
}

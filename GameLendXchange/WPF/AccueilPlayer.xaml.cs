using GameLendXchange.Classes;
using GameLendXchange.Classes.ViewModel;
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
    /// Logique d'interaction pour AccueilPlayer.xaml
    /// </summary>
    public partial class AccueilPlayer : Page
    {
        public AccueilPlayer()
        {
            InitializeComponent();
            DataContext = new AccueilPlayerViewModel();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainWindow());
        }

        private void LocationsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainWindow());
        }

        private void ReservationsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainWindow());
        }

        private void EditPlayerButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

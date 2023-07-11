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
using GameLendXchange.WPF;
using GameLendXchange.Classes;
using GameLendXchange;

namespace GameLendXchange
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           



        }

        private void dgGame_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Vérifiez s'il y a un élément sélectionné dans le DataGrid
            if (dgGame.SelectedItem != null)
            {
                // Obtenez l'élément sélectionné
                var selectedGame = dgGame.SelectedItem as VideoGame;

                // Créez une instance de la page de destination et transmettez les informations
                InfoGame destinationPage = new InfoGame(selectedGame);
                MainFrame.Navigate(destinationPage);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            // string buttonContent = button.Content.ToString(); utilisation du name plutot que du content car nous n'en avions pas defini
            string buttonContent = button.Name.ToString();

            switch (buttonContent)
            {
                case "Playstation":
                    List<VideoGame> playstation = VideoGame.GetGamesByPlaystation();
                    dgGame.ItemsSource = playstation;
                    break;
                case "XBOX":
                    List<VideoGame> xbox = VideoGame.GetGamesByXbox();
                    dgGame.ItemsSource = xbox;
                    break;
                case "NINTENDO":
                    List<VideoGame> nintendo = VideoGame.GetGamesByNintendo();
                    dgGame.ItemsSource = nintendo;
                    break;
            }
        }

        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
           //MainFrame.Navigate(new Registration());
            MainFrame.Navigate(new Registration());
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Login());
        }
    }
}

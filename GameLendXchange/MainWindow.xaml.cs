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
            ConfigureDataGridColumns();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
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
            MainFrame.Navigate(new Registration());
            HomeGrid.Visibility = Visibility.Collapsed;
            ContentGrid.Visibility = Visibility.Visible;
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Login());
            HomeGrid.Visibility = Visibility.Collapsed;
            ContentGrid.Visibility = Visibility.Visible;
        }

        private void ConfigureDataGridColumns()
        {
            
            dgGame.Columns.Clear(); 

            // Colonne pour l'ID du jeu
            DataGridTextColumn idColumn = new DataGridTextColumn();
            idColumn.Header = "ID";
            idColumn.Binding = new Binding("IdGame");
            dgGame.Columns.Add(idColumn);

            // Colonne pour le nom du jeu
            DataGridTextColumn nameColumn = new DataGridTextColumn();
            nameColumn.Header = "Nom du jeu";
            nameColumn.Binding = new Binding("Name");
            dgGame.Columns.Add(nameColumn);

            // Colonne pour le coût en crédits
            DataGridTextColumn creditCostColumn = new DataGridTextColumn();
            creditCostColumn.Header = "Coût en crédits";
            creditCostColumn.Binding = new Binding("CreditCost");
            dgGame.Columns.Add(creditCostColumn);

            // Colonne pour la console
            DataGridTextColumn consoleColumn = new DataGridTextColumn();
            consoleColumn.Header = "Console";
            consoleColumn.Binding = new Binding("Console");
            dgGame.Columns.Add(consoleColumn);

            List<VideoGame> videoGames = VideoGame.GetGames();
            dgGame.ItemsSource = videoGames;
        }
    }
}

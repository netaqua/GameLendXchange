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
    public partial class AccueilPlayer : Page
    {
        public AccueilPlayer(Player p)
        {
            InitializeComponent();
            DataContext = new AccueilPlayerViewModel();
             ConfigureDataGridColumnsGames();
             ConfigureDataGridColumnsBooking();
             ConfigureDataGridColumnsLoan();
        }

        //-------------------------------------------------------------------------------//
        //******************** GESTION DES DATA GRID ************************************//
        //-------------------------------------------------------------------------------//

        private void ConfigureDataGridColumnsGames()
        {

            dgGame.Columns.Clear(); 

            DataGridTextColumn idColumn = new DataGridTextColumn();
            idColumn.Header = "ID";
            idColumn.Binding = new Binding("IdGame");
            dgGame.Columns.Add(idColumn);

            DataGridTextColumn nameColumn = new DataGridTextColumn();
            nameColumn.Header = "Nom du jeu";
            nameColumn.Binding = new Binding("Name");
            dgGame.Columns.Add(nameColumn);

            DataGridTextColumn creditCostColumn = new DataGridTextColumn();
            creditCostColumn.Header = "Coût en crédits";
            creditCostColumn.Binding = new Binding("CreditCost");
            dgGame.Columns.Add(creditCostColumn);

            DataGridTextColumn consoleColumn = new DataGridTextColumn();
            consoleColumn.Header = "Console";
            consoleColumn.Binding = new Binding("Console");
            dgGame.Columns.Add(consoleColumn);

            List<VideoGame> videoGames = VideoGame.GetGames();
            dgGame.ItemsSource = videoGames;
        }

        private void ConfigureDataGridColumnsBooking()
        {
            dgBooking.Columns.Clear();

            DataGridTextColumn idColumn = new DataGridTextColumn();
            idColumn.Header = "ID Réservation";
            idColumn.Binding = new Binding("IdBooking");
            dgBooking.Columns.Add(idColumn);

            DataGridTextColumn dateColumn = new DataGridTextColumn();
            dateColumn.Header = "Date de réservation";
            dateColumn.Binding = new Binding("BookingDate");
            dgBooking.Columns.Add(dateColumn);

            DataGridTextColumn idGameColumn = new DataGridTextColumn();
            idGameColumn.Header = "ID Jeu";
            idGameColumn.Binding = new Binding("IdVideoGame");
            dgBooking.Columns.Add(idGameColumn);

            List<Booking> bookings= new List<Booking>();
            
            dgBooking.ItemsSource = bookings;
        }

        private void ConfigureDataGridColumnsLoan()
        {
            dgLoan.Columns.Clear();

            DataGridTextColumn idColumn = new DataGridTextColumn();
            idColumn.Header = "ID Location";
            idColumn.Binding = new Binding("IdLocation");
            dgLoan.Columns.Add(idColumn);

            DataGridTextColumn startDateColumn = new DataGridTextColumn();
            startDateColumn.Header = "Date de début";
            startDateColumn.Binding = new Binding("StartDate");
            dgLoan.Columns.Add(startDateColumn);

            DataGridTextColumn endDateColumn = new DataGridTextColumn();
            endDateColumn.Header = "Date de fin";
            endDateColumn.Binding = new Binding("EndDate");
            dgLoan.Columns.Add(endDateColumn);

            DataGridTextColumn activityColumn = new DataGridTextColumn();
            activityColumn.Header = "Activité";
            activityColumn.Binding = new Binding("Activity");
            dgLoan.Columns.Add(activityColumn);

            DataGridTextColumn idBorrowerColumn = new DataGridTextColumn();
            idBorrowerColumn.Header = "ID Emprunteur";
            idBorrowerColumn.Binding = new Binding("IdBorrower");
            dgLoan.Columns.Add(idBorrowerColumn);

            DataGridTextColumn idCopyColumn = new DataGridTextColumn();
            idCopyColumn.Header = "Id Copie";
            idCopyColumn.Binding = new Binding("IdCopy");
            dgLoan.Columns.Add(idCopyColumn);

            List<Loan> loans = new List<Loan>();

            dgLoan.ItemsSource = loans;
        }

        //-------------------------------------------------------------------------------//
        //******************** GESTION DES BOUTONS **************************************//
        //-------------------------------------------------------------------------------//

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();
        }

        private void BookingButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LocationsButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        //private void EditPlayerButton_Click(object sender, RoutedEventArgs e)
        //{
            
        //}
    }
}

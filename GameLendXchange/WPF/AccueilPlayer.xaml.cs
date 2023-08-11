using GameLendXchange.Classes;
using GameLendXchange.Classes.ViewModel;
using GameLendXchange.DAO;
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
using static GameLendXchange.Classes.ViewModel.AccueilPlayerViewModel;

namespace GameLendXchange.WPF
{
    public partial class AccueilPlayer : Page
    {
        public Player player;
        public AccueilPlayer(Player p)
        {

            InitializeComponent();
            player = p;
            AccueilPlayerViewModel viewModel = new AccueilPlayerViewModel(p);
            Player.GetBirthdayBonus(p);

            DataContext = viewModel;

            ConfigureDataGridColumnsGames();
            ConfigureDataGridColumnsBooking();
            ConfigureDataGridColumnsLoan();
        }

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

            List<Booking> bookings= Booking.GetBookingsPlayer(player.IdUser);

            List<BookingViewModel> bookingViewModels = bookings.Select(booking => new BookingViewModel
            {
                IdBooking = booking.IdBooking,
                BookingDate = booking.BookingDate,
                IdVideoGame = booking.VideoGame.IdGame
            }).ToList();

            dgBooking.ItemsSource = bookingViewModels;
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
            activityColumn.Header = "Etat location";
            activityColumn.Binding = new Binding("OnGoing");
            dgLoan.Columns.Add(activityColumn);

            DataGridTextColumn idBorrowerColumn = new DataGridTextColumn();
            idBorrowerColumn.Header = "ID Emprunteur";
            idBorrowerColumn.Binding = new Binding("BorrowerId");
            dgLoan.Columns.Add(idBorrowerColumn);

            DataGridTextColumn idCopyColumn = new DataGridTextColumn();
            idCopyColumn.Header = "Id Copie";
            idCopyColumn.Binding = new Binding("IdCopy");
            dgLoan.Columns.Add(idCopyColumn);

            List<Loan> loans = Loan.GetLoansById(player.IdUser);
            List<LoanViewModel> loanViewModels = loans.Select(loan => new LoanViewModel
            {
                IdLocation = loan.IdLoan,
                StartDate = loan.StartDate,
                EndDate = loan.EndDate,
                OnGoing = loan.OnGoing,
                BorrowerId = loan.Borrower.IdUser,
                LenderId = loan.Lender.IdUser,
                IdCopy = loan.Copy.IdCopy 
            }).ToList();

            dgLoan.ItemsSource = loanViewModels;
        }

        private void dgGame_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgGame.SelectedItem is VideoGame selectedGame)
            {
                NavigationService?.Navigate(new InfoGame(selectedGame.IdGame));
            }
        }

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
            SetOnLocations setLocationPage = new SetOnLocations(player);

            setLocationPage.Tag = "Informations supplémentaires si nécessaires";

            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(setLocationPage);
            }
        }

        private void dgLoan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgLoan.SelectedItem is AccueilPlayerViewModel.LoanViewModel selectedLoan)
            {
                MessageBoxResult result = MessageBox.Show($"Voulez-vous stopper la location ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    LoanDB loanDB = new LoanDB();
                    bool succes2 = loanDB.CalculateBalance(selectedLoan.IdLocation);
                    if (succes2)
                    {
                        bool success = loanDB.EndLoan(selectedLoan.IdLocation);
                        if (success)
                        {
                            int cId = selectedLoan.IdCopy;
                            Copy c = new Copy();
                            c.ReleaseCopy(cId);
                            MessageBox.Show("Location stoppée ! (actualiser la page)");
                        }
                        else
                        {
                            MessageBox.Show("Erreur : Impossible de stopper cette location !");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Action annulée !");
                }
            }
        }
    }
}

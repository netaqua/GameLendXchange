using GameLendXchange.Classes;
using GameLendXchange.WPF.ViewModel;
using GameLendXchange.Classes;
using GameLendXchange.WPF.ViewModel;
using System.Windows.Controls;

namespace GameLendXchange.WPF
{
    public partial class InfoGame : Page
    {
        public InfoGame(int gameId)
        {
            InitializeComponent();

            // Create the ViewModel for InfoGame
            InfoGameViewModel viewModel = new InfoGameViewModel(gameId);

            // Set the DataContext of the page to the ViewModel
            DataContext = viewModel;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
    /*
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

            List<Booking> bookings = Booking.GetBookingsPlayer(player.IdUser);

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
                IdCopy = loan.Copy.IdCopy // Remplacez "IdCopy" par la propriété appropriée pour obtenir l'ID de la copie
            }).ToList();

            dgLoan.ItemsSource = loanViewModels;
        }

    


    }*/
}

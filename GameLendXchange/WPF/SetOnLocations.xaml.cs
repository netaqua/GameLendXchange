using GameLendXchange.Classes;
using GameLendXchange.Classes.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
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
    /// Logique d'interaction pour setOnLocations.xaml
    /// </summary>
    public partial class SetOnLocations : Page
    {
        public Player player { get; }
       
        public SetOnLocations(Player p)
        {
            InitializeComponent();
            ConfigureDataGridColumns();
            player = p;
            AccueilPlayerViewModel viewModel = new AccueilPlayerViewModel(p);
        }

         private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AccueilPlayer(player));
        }

        private void ConfigureDataGridColumns()
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

        public void ClearText()
        {
            validateMessage.Text = "";
            errorMessage.Text = "";
        }

        // METHODE DE DEPÔT D'UN JEU //

        private void DepositBtn_Click(object sender, RoutedEventArgs e)
        {
            Copy c = new Copy();
            int i;

            if (dgGame.SelectedItem is VideoGame selectedGame) 
            {
                VideoGame videoGame = VideoGame.ReadId(selectedGame.IdGame);

                if (videoGame != null)
                {
                    c.VideoGame = videoGame;
                    c.Owner = player;

                    bool success = c.Insert();
                    c = c.ReadByOwnerAndGame(videoGame.IdGame, player.IdUser);

                    if (success)
                    {
                        List<Booking> bookings = Booking.GetBookingsVideoGame(selectedGame.IdGame);
                        bool successLocation = false;

                        if(bookings.Count > 0)
                        {
                            Booking selectedBooking = bookings
                            .OrderByDescending(b => b.Player.Credit)
                            .ThenBy(b => b.BookingDate)
                            .ThenBy(b => b.Player.RegistrationDate)
                            .ThenBy(b => b.Player.DateOfBirth)
                            .FirstOrDefault();

                            Loan lNew= new Loan();
                            lNew.Copy = c;
                            lNew.Borrower = selectedBooking.Player;
                            lNew.StartDate = DateTime.Now;
                            lNew.EndDate = DateTime.Now.AddDays(7);
                            lNew.Lender = player;
                            lNew.OnGoing= true;
                           

                            if(selectedBooking.VideoGame.CreditCost < lNew.Borrower.Credit)
                            {
                                bool succesLocation = lNew.insert();
                                lNew.Borrower.Credit -= videoGame.CreditCost;
                                lNew.Borrower.Update(lNew.Borrower);
                                if(succesLocation)
                                {
                                    bool DelBook = selectedBooking.Delete();
                                    if(DelBook)
                                    {
                                        ClearText();
                                        validateMessage.Text = "Votre jeu a directement été emprunté par un joueur";
                                        c.Borrow();
                                    }
                                    
                                }
                            }
                            else
                            {
                                ClearText();
                                errorMessage.Text = "L'emprunteur n'a pas assez de credit";
                            }


                        }
                        else
                        {
                            ClearText();
                            validateMessage.Text = "Jeu déposé avec succès";
                            errorMessage.Text = string.Empty;
                        }
                    }
                    else
                    {
                        ClearText();
                        errorMessage.Text = "Erreur lors de l'insertion d'une copie. Veuillez vérifier les informations et réessayez.";
                    }
                }
                else
                {
                    ClearText();
                    errorMessage.Text = "Erreur";
                }
            }
            else
            {
                ClearText();
                errorMessage.Text = "Veuillez sélectionner un jeu dans la liste.";
            }
        }


        // METHODE DE LOCATION D'UN JEU //

        
         private void LoanBtn_Click(object sender, RoutedEventArgs e)
         {
            if (dgGame.SelectedItem is VideoGame selectedGame)
            {
                List<Copy> copies= Copy.GetCopiesByVideoGame(selectedGame.IdGame);
                Loan loan = new Loan();
                 Copy copieFind = selectedGame.CopyAvailable(copies);
                 if (copieFind != null)
                 {
                     if (player.LoanAllowed(selectedGame.CreditCost,player))
                     {
                         loan.Copy = copieFind;
                         loan.Borrower = player;
                         loan.Lender = copieFind.Owner;
                         loan.OnGoing = true;
                         DateTime now = DateTime.Now;
                         loan.StartDate = now;
                         loan.EndDate = now.AddDays(7);
                         player.Credit -= copieFind.VideoGame.CreditCost;

                         var succes = player.Update(player);
                         if (succes)
                         {
                             copieFind.Borrow();
                             loan.insert();
                             loan.Lender.Credit += copieFind.VideoGame.CreditCost;
                             var succesP2 = player.Update(loan.Lender);
                             ClearText();
                             validateMessage.Text = "Location réalisé";
                         }
                         else
                         {
                             errorMessage.Text = "Erreur lors de la modification des crédits";
                         }
                     }
                     else
                     {
                        ClearText();
                        errorMessage.Text = "Pas assez de crédits pour le jeu";
                     }
                 }
                 else
                 {

                     Booking booking = new Booking();
                     booking.Player = player;
                     booking.VideoGame = VideoGame.ReadId(selectedGame.IdGame);
                     DateTime now= DateTime.Now;
                     booking.BookingDate = now;
                     var succesBook = booking.Insert();
                     if (succesBook)
                     {
                        ClearText();
                        errorMessage.Text = "Copie non disponible pour le moment, réservation réalisée";
                     }
                     else
                     {
                        ClearText();
                        errorMessage.Text = "Copie non disponible pour le moment, mais réservation impossible";
                     }
                 }
             }
         }
    }
}

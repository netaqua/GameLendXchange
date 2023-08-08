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
            player = p;
            AccueilPlayerViewModel viewModel = new AccueilPlayerViewModel(p);
        }

         private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AccueilPlayer(player));
        }

        private void DepositBtn_Click(object sender, RoutedEventArgs e)
        {
            Copy c = new Copy();

            if (int.TryParse(idGameText.Text, out int gameId))
            {
                VideoGame videoGame = VideoGame.GetGameById(gameId);

                if (videoGame != null)
                {
                    c.VideoGame = videoGame;

                    c.Owner = player;

                    bool success = c.Insert();

                    if (success)
                    {
                       ClearFields();
                       errorMessage.Text = string.Empty;
                    }
                    else
                    {
                       errorMessage.Text = "Erreur lors de l'insertion d'une copie. Veuillez vérifier les informations et réessayez.";
                    }
                }
                else
                {
                    errorMessage.Text = "Aucun jeu trouvé avec cet ID.";
                }
            }
            else
            {
                errorMessage.Text = "L'ID du jeu doit être un entier valide.";
            }
        }


        private void ClearFields()
        {
            idGameText = null;
        }

        private void LoanBtn_Click(object sender, RoutedEventArgs e)
        {
            if(int.TryParse(idGameText.Text, out int gameId))
            {
                List<Copy> copies= Copy.GetCopiesByVideoGame(gameId);
                Loan loan= new Loan();
                Copy copieFind = copies.Find(c =>c.Available==true);
                if (copieFind != null)
                {
                    if (copieFind.VideoGame.CreditCost <= player.Credit)
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
                            copieFind.Available = false;
                            copieFind.Update();
                            loan.insert();
                            loan.Lender.Credit += copieFind.VideoGame.CreditCost;
                            var succesP2 = player.Update(loan.Lender);
                            validateMessage.Text = "Location réalisé";
                        }
                        else
                        {
                            errorMessage.Text = "Erreur lors de la modification des crédits";
                        }
                    }
                    else
                    {
                        errorMessage.Text = "Pas assez de crédit pour le jeu";
                    }
                }
                else
                {
                    
                    Booking booking = new Booking();
                    booking.Player = player;
                    booking.VideoGame = VideoGame.ReadId(gameId);
                    DateTime now= DateTime.Now;
                    booking.BookingDate = now;
                    var succesBook = booking.Insert();
                    if (succesBook)
                    {
                        errorMessage.Text = "Copie non dispo pour le moment, réservation réalisée";
                    }
                    else
                    {
                        errorMessage.Text = "Copie non dispo pour le moment, mais réservation impossible";
                    }
                }
            }
        }
    }
}

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

        private void ValidationBtn_Click(object sender, RoutedEventArgs e)
        {
            Copy c = new Copy();

            if (int.TryParse(idGameText.Text, out int gameId))
            {
                c.IdCopy = gameId;

                // Utilisez votre méthode pour récupérer l'objet VideoGame à partir de l'ID
                VideoGame videoGame = VideoGame.GetGameById(gameId);

                if (videoGame != null)
                {
                    c.VideoGame = videoGame;

                    if (int.TryParse(idPlayerText.Text, out int playerId))
                    {
                        // Utilisez votre méthode pour récupérer l'objet Player à partir de l'ID
                        Player ownerPlayer = Player.GetPlayerById(playerId);

                        if (ownerPlayer != null)
                        {
                            c.Owner = ownerPlayer;

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
                            errorMessage.Text = "Aucun joueur trouvé avec cet ID.";
                        }
                    }
                    else
                    {
                        errorMessage.Text = "L'ID du joueur doit être un entier valide.";
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
            idPlayerText = null;
        }
    }
}

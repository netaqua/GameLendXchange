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
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            Player p = new Player();
            p.Username = usernameTextBox.Text;
            p.Password = passwordTextBox.Password;
            p.DateOfBirth = dateOfBirthSelect.SelectedDate.Value;
            p.Pseudo = pseudoTextBox.Text;
            p.RegistrationDate = DateTime.Now;

            bool success = p.Insert();

            if (success)
            {
                // Enregistrement réussi, vous pouvez rediriger l'utilisateur vers la page de connexion ou effectuer une autre action
                NavigationService.Navigate(new Login());

                // Effacer les champs de saisie
                ClearFields();

                // Effacer le message d'erreur
                errorMessage.Text = string.Empty;
            }
            else
            {
                // Afficher un message d'erreur
                errorMessage.Text = "Erreur lors de la création du joueur. Veuillez vérifier vos informations et réessayer.";
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();
        }

        private void ClearFields()
        {
            // Effacer les champs de saisie
            usernameTextBox.Text = string.Empty;
            passwordTextBox.Password = string.Empty;
            dateOfBirthSelect.SelectedDate = null;
            pseudoTextBox.Text = string.Empty;
        }

    }
}

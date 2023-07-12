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
    /// <summary>
    /// Logique d'interaction pour Registration.xaml
    /// </summary>
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
            p.Insert();


            // Rediriger l'utilisateur vers la page de connexion après l'enregistrement
            //NavigationService.Navigate(new Login());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainWindow());
        }

    }
}

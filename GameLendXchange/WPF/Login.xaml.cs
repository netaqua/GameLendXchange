using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        private String connectionString;
        public Login()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["XchangeDB"].ConnectionString;
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            
            NavigationService.Navigate(new MainWindow());
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

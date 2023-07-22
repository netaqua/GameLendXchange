using GameLendXchange.Classes;
using GameLendXchange.WPF.ViewModel;
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
    /// Logique d'interaction pour InfoGame.xaml
    /// </summary>
    public partial class InfoGame : Page
    {

        private int selectedGameID;
        public InfoGame(int gameID)
        {
            InitializeComponent();
            InfoGameViewModel viewModel = new InfoGameViewModel(gameID);
            DataContext = viewModel;
        }




    }
}

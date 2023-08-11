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

            InfoGameViewModel viewModel = new InfoGameViewModel(gameId);

            DataContext = viewModel;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

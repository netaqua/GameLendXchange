using GameLendXchange.Classes;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace GameLendXchange.WPF.ViewModel
{
    public class InfoGameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private VideoGame selectedGame;

        public InfoGameViewModel(int gameId)
        {
            // Fetch the game information based on the provided gameId
            selectedGame = VideoGame.GetGameById(gameId);
        }

        public string Name
        {
            get => selectedGame?.Name;
            set
            {
                if (selectedGame != null)
                {
                    selectedGame.Name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public int CreditCost
        {
            get => selectedGame?.CreditCost ?? 0;
            set
            {
                if (selectedGame != null)
                {
                    selectedGame.CreditCost = value;
                    OnPropertyChanged(nameof(CreditCost));
                }
            }
        }

        public string Console
        {
            get => selectedGame?.Console;
            set
            {
                if (selectedGame != null)
                {
                    selectedGame.Console = value;
                    OnPropertyChanged(nameof(Console));
                }
            }
        }

        public ObservableCollection<Copy> Copies => selectedGame?.Copies != null ? new ObservableCollection<Copy>(selectedGame.Copies) : new ObservableCollection<Copy>();

        public ObservableCollection<Booking> Bookings => selectedGame?.Bookings != null ? new ObservableCollection<Booking>(selectedGame.Bookings) : new ObservableCollection<Booking>();

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

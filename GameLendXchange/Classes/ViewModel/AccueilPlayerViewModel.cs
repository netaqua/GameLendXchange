using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace GameLendXchange.Classes.ViewModel
{
    internal class AccueilPlayerViewModel : INotifyPropertyChanged
    {
        private Player currentPlayer;
        private ObservableCollection<VideoGame> gameList;

        public Player CurrentPlayer
        {
            get { return currentPlayer; }
            set
            {
                currentPlayer = value;
                OnPropertyChanged(nameof(CurrentPlayer));
            }
        }

        public ObservableCollection<VideoGame> GameList
        {
            get { return gameList; }
            set
            {
                gameList = value;
                OnPropertyChanged(nameof(GameList));
            }
        }

        public void HomePageViewModel()
        {
            // Initialisez les données nécessaires pour la page d'accueil
            LoadCurrentPlayer();
            LoadGameList();
            
        }

        private void LoadCurrentPlayer()
        {
            // Effectuez les opérations nécessaires pour récupérer les informations du joueur actuel
            // par exemple, à partir d'une source de données ou d'un service
            CurrentPlayer = new Player(); // Remplacez par la logique appropriée pour charger le joueur actuel
        }

        private void LoadGameList()
        {
            // Effectuez les opérations nécessaires pour récupérer la liste des jeux
            // par exemple, à partir d'une source de données ou d'un service
            GameList = new ObservableCollection<VideoGame>(); // Remplacez par la logique appropriée pour charger la liste des jeux
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

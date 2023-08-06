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
    public class AccueilPlayerViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int playerID;

        public int PlayerID
        {
            get { return playerID; }
            set
            {
                playerID = value;
                OnPropertyChanged(nameof(PlayerID));
            }
        }

        private int playerCredit;
        public int PlayerCredit
        {
            get { return playerCredit; }
            set
            {
                playerCredit = value;
                OnPropertyChanged(nameof(PlayerCredit));
            }
        }
        
        private String playerPseudo;
        public String PlayerPseudo
        {
            get { return playerPseudo; }
            set
            {
                playerPseudo = value;
                OnPropertyChanged(nameof(PlayerPseudo));
            }
        }

        private DateTime playerRegistrationDate;
        public DateTime PlayerRegistrationDate
        {
            get { return playerRegistrationDate; }
            set
            {
                playerRegistrationDate = value;
                OnPropertyChanged(nameof(PlayerRegistrationDate));
            }
        }

        private DateTime playerDateOfBirth;
        public DateTime PlayerDateOfBirth
        {
            get { return playerDateOfBirth; }
            set
            {
                playerDateOfBirth = value;
                OnPropertyChanged(nameof(playerDateOfBirth));
            }
        }

        

        public AccueilPlayerViewModel(Player currentPlayer)
        {
            PlayerPseudo = currentPlayer.Pseudo;
            PlayerCredit = currentPlayer.Credit;
            PlayerRegistrationDate = currentPlayer.RegistrationDate;
            PlayerDateOfBirth = currentPlayer.DateOfBirth;

            
        }

  
    }
}


using GameLendXchange.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLendXchange.Classes
{
    public class Player : User
    {
        public int Credit { get; set; }
        public string Pseudo { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Player(int idPlayer, string pseudo, string username, string password, DateTime registrationDate, DateTime dateOfBirth)
        {
            IdUser = idPlayer;
            Credit = 10;
            Pseudo = pseudo;
            Username = username;
            Password = password;
            RegistrationDate = registrationDate;
            DateOfBirth = dateOfBirth;
        }

        public Player()
        {
        }

        public override string ToString()
        {
            return $"Id Player : {IdUser}, Balance : {Credit}, Pseudo : {Pseudo}, Username : {Username}, Registration Date : {RegistrationDate}, Date of Birthday : {DateOfBirth}";
        }

        public static List<Player> GetPlayers()
        {
            PlayerDB dB = new PlayerDB();
            return dB.ReadAll();
        }


        public bool Insert()
        {
            PlayerDB db = new PlayerDB();
            return db.Insert(this);
        }

    }
}

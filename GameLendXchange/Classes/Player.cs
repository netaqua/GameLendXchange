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
        public bool IsBirthdayBonusReceived { get; set; }


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
            return $"Id Player : {IdUser}, Pseudo : {Pseudo}";
        }

        public static List<Player> GetPlayers()
        {
            PlayerDB dB = new PlayerDB();
            return dB.ReadAll();
        }

        public Player GetPlayer(string username, string password) 
        {
            PlayerDB dB = new PlayerDB();
            return dB.GetConnection(username, password);
        }


        public bool Insert()
        {
            PlayerDB db = new PlayerDB();
            return db.Create(this);
        }

        public static Player GetPlayerById(int idPlayer)
        {
            PlayerDB dB = new PlayerDB();
            return dB.GetPlayerById(idPlayer);
        }

        public static void GetBirthdayBonus(Player player)
        {
            if (!player.IsBirthdayBonusReceived)
            {
                PlayerDB.AddBirthdayBonus(player);
            }
            else
            {
                Console.WriteLine("Bonus déjà reçu ... ");
            }
        }

        public bool Update(Player player)
        {
            PlayerDB db = new PlayerDB();
            return db.Update(player);
        }

        public bool LoanAllowed(int creditCost, Player p)
        {
            PlayerDB db = new PlayerDB();
            return db.LoanAllowed(creditCost, p);
        }

    }
}

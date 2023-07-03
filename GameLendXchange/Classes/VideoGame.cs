using GameLendXchange.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameLendXchange.Classes
{
    public class VideoGame
    {
        public int IdGame { get; set; }
        public string Name { get; set; }
        public int CreditCost { get; set; }
        
        public string Console { get; set; }
        public List<Copy> Copies { get; set; }
        public List<Booking> Bookings { get; set; }


        public VideoGame(int idGame, string name, int creditCost, string console)
        {
            IdGame = idGame;
            Name = name;
            CreditCost = creditCost;
            Console = console;
        }

        public VideoGame() { }

        public override string ToString()
        {
            return $"Id Game : {IdGame}, Name : {Name}, Credit Cost : {CreditCost}, Console : {Console}";
        }

        public static List<VideoGame> GetGames()
        {
            VideoGameDB dB = new VideoGameDB();
            return dB.ReadAll();
        }

        public static List<VideoGame> GetGamesByPlaystation()
        {
            VideoGameDB dB = new VideoGameDB();
            return dB.SortByPlaystation();
        }
        public static List<VideoGame> GetGamesByXbox()
        {
            VideoGameDB dB = new VideoGameDB();
            return dB.SortByXbox();
        }

        public static List<VideoGame> GetGamesByNintendo()
        {
            VideoGameDB dB = new VideoGameDB();
            return dB.SortByNintendo();
        }

    }
}

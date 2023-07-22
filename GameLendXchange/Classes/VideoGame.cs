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
        public List<Copy> Copies { get; set; } = new List<Copy>();
        public List<Booking> Bookings { get; set; } = new List<Booking>();


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

        public static VideoGame ReadId(int idGame)
        {
            VideoGameDB dB = new VideoGameDB();
            return dB.ReadId(idGame);
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

        public static VideoGame GetGameById(int idGame)
        {
            VideoGameDB db = new VideoGameDB();
            VideoGame videoGame = db.ReadById(idGame);

            if (videoGame != null)
            {
                // Récupérer les copies associées au jeu
                List<Copy> copies = Copy.GetCopiesByVideoGame(idGame);
                videoGame.Copies = copies;

                // Récupérer les réservations associées au jeu
                List<Booking> bookings = Booking.GetBookingsByVideoGame(idGame);
                videoGame.Bookings = bookings;
            }

            return videoGame;
        }


    }
}

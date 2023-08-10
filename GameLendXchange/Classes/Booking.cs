using GameLendXchange.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameLendXchange.Classes
{
    public class Booking
    {
        public int IdBooking { get; set; }
        public DateTime BookingDate { get; set; }
        public Player Player { get; set; }
        public VideoGame VideoGame { get; set; }

        public static List<Booking> GetBookings()
        {
            BookingDB dB = new BookingDB();
            return dB.ReadAll();
        }

        public static List<Booking> GetBookingsPlayer(int idPlayer)
        {
            BookingDB dB = new BookingDB();
            return dB.ReadAllBookingPlayer(idPlayer);
        }
        public static List<Booking> GetBookingsVideoGame(int idGame)
        {
            BookingDB dB = new BookingDB();
            return dB.ReadAllBookingVideoGame(idGame);
        }

        public bool Insert()
        {
            BookingDB db = new BookingDB();
            return db.Create(this);
        }
    }
}

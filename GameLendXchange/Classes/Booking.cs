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
        public DateTime BookingDate { get; set; }
        public Player Player { get; set; }
        public VideoGame VideoGame { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLendXchange.Classes
{
    public class VideoGame
    {
        public string Name { get; set; }
        public int CreditCost { get; set; }
        public string Console { get; set; }
        public List<Copy> Copies { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}

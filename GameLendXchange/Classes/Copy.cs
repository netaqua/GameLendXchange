using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameLendXchange.Classes
{
    public class Copy
    {
        public Player Owner { get; set; }
        public List<Loan> Loans { get; set; }
        public VideoGame VideoGame { get; set; }
    }
}

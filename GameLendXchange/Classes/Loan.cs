using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameLendXchange.Classes
{
    public class Loan
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsOngoing { get; set; }
        public Player Lender { get; set; }
        public Player Borrower { get; set; }
        public Copy Copy { get; set; }
    }
}

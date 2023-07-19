using GameLendXchange.DAO;
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
        public int IdLoan { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool OnGoing { get; set; }
        public Player Lender { get; set; }
        public Player Borrower { get; set; }
        public Copy Copy { get; set; }

        public static List<Loan> GetLoans()
        {
            LoanDB dB = new LoanDB();
            return dB.ReadAll();
        }
    }
}

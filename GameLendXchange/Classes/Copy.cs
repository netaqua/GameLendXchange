using GameLendXchange.DAO;
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
        public int IdCopy { get; set; }
        public Player Owner { get; set; }
        public List<Loan> Loans { get; set; }
        public VideoGame VideoGame { get; set; }

        public bool Available { get; set; }

        public static List<Copy> GetCopiesByVideoGame(int idGame)
            {
                CopyDB db = new CopyDB();
                return db.ReadAllCopiesByVideoGame(idGame);
            }

        public bool Insert()
        {
            CopyDB db = new CopyDB();
            return db.Create(this);
        }

        public bool Update()
        {
            CopyDB db = new CopyDB();
            return db.Update(this);
        }

        public void RealeaseCopy()
        {

        }

        public void Borrow() { }

        public bool IsAvailable(Loan l)
        {
            if (l.OnGoing)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"Id Copy : {IdCopy}";
        }


    }

    
}

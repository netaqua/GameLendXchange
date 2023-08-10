using GameLendXchange.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
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

        public Copy ReadByOwnerAndGame(int idGame, int idOwner)
        {
            CopyDB dB= new CopyDB();
            return dB.Read(idGame, idOwner);
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

        public void Borrow() {
            CopyDB db = new CopyDB();
            db.Borrow(this);
        }

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

        public void ReleaseCopy(int cId)
        {
            CopyDB db = new CopyDB();
            db.ReleaseCopy(cId);
        }

        public override string ToString()
        {
            return $"Id Copy : {IdCopy}";
        }


    }

    
}

﻿using GameLendXchange.DAO;
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

        public static List<Copy> GetCopiesByVideoGame(int idGame)
            {
                CopyDB db = new CopyDB();
                return db.ReadAllCopiesByVideoGame(idGame);
            }

    }

    
}

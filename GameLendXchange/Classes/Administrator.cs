using GameLendXchange.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLendXchange.Classes
{
    public class Administrator : User
    {
        public Administrator(int idAdmin, string username, string password) { 
            IdUser= idAdmin;
            Username= username;
            Password= password;
        }

        public Administrator(){}

        public override string ToString()
        {
            return $"Id Admin : {IdUser}, Username : {Username}";
        }
    }
}

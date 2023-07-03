using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLendXchange.Classes
{
    public abstract class User
    {
        public int IdUser { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

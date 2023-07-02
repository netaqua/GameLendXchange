using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLendXchange.Classes
{
    public class Player : User
    {
        public int Credit { get; set; }
        public string Pseudo { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}

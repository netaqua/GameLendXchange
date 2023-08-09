using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLendXchange.Classes
{
    public class User
    {
        public int IdUser { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User Login(string username, string password)
        {
            Administrator administrator= new Administrator();
            administrator = administrator.GetAdministrator(username, password);
            if(administrator != null)
            {
                return administrator;
            }
            else
            {
                Player player= new Player();
                player = player.GetPlayer(username, password);
                if(player != null)
                {
                    return player;
                }
                else
                {
                    return null;
                }
            }
        }
    }

    
}

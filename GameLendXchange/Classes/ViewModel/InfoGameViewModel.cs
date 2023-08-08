using GameLendXchange.Classes;
using System.Collections.ObjectModel;

namespace GameLendXchange.WPF.ViewModel
{
    public class InfoGameViewModel
    {
        public string Name { get; set; }
        public int CreditCost { get; set; }
        public string Console { get; set; }
        public ObservableCollection<Copy> Copies { get; set; } = new ObservableCollection<Copy>();
        public ObservableCollection<Booking> Bookings { get; set; } = new ObservableCollection<Booking>();

        public InfoGameViewModel(int gameId)
        {
            // Fetch the game information based on the provided gameId
            VideoGame game = VideoGame.GetGameById(gameId);
            if (game != null)
            {
                Name = game.Name;
                CreditCost = game.CreditCost;
                Console = game.Console;

                // Populate the Copies and Bookings collections
                foreach (Copy copy in game.Copies)
                {
                    Copies.Add(copy);
                }

                foreach (Booking booking in game.Bookings)
                {
                    Bookings.Add(booking);
                }
            }
        }
    }
}

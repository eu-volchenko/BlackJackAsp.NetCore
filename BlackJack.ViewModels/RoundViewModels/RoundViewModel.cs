using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack.ViewModels.RoundViewModels
{
    public class RoundViewModel
    {
        public int id { get; set; }
        public int roundInGame { get; set; }
        public int gameId { get; set; }
        public string winnerName { get; set; }
    }
}

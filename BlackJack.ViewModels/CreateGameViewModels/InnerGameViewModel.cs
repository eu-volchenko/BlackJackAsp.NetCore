using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack.ViewModels.CreateGameViewModels
{
    public class InnerGameViewModel
    {
        public int id { get; set; }
        public string playerName { get; set; }
        public int numberOfBots { get; set; }
        public string dealerName { get; set; }
        public List<string> nameOfBots { get; set; } = new List<string>();
    }
}

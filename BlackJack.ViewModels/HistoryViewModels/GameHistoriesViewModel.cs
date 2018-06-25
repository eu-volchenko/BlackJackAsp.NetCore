using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack.ViewModels.HistoryViewModels
{
    public class GameHistoriesViewModel
    {
        public DateTime DateTimeGame { get; set; }
        public int Id { get; set; }
        public int CountOfBots { get; set; }
    }
}

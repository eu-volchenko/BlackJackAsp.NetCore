using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack.ViewModels.HistoryViewModels
{
    public class PlayerCardHistoryViewModel
    {
        public string PlayerName { get; set; }
        public List<int> CardsId { get; set; }
    }
}

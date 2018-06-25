using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack.ViewModels.RoundViewModels
{
    public class WinnerViewModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public IEnumerable<int> Cards { get; set; }
    }
}

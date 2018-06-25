using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack.ViewModels.RoundViewModels
{
    public class UserCardsViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<int> cards { get; set; } = new List<int>();
    }
}

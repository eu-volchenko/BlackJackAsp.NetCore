using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack.ViewModels.RoundViewModels
{
    public class CardViewModel
    {
        public int Id { get; set; }

        public string Amount { get; set; }

        public string Suit { get; set; }

        public string Cost { get; set; }
    }
}

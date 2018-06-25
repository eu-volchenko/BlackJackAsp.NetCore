using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack.BLL.DTO
{
    class HistoryDTO
    {
        public int Id { get; set; }

        public DateTime LogDateTime { get; set; }

        public int GameId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack.BLL.DTO
{
    class UserDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TypeId { get; set; }

        public int GameId { get; set; }
    }
}

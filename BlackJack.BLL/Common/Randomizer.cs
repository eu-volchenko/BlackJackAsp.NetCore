using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack.BLL.Common
{
    static class Randomizer
    {

        public static int RandomId()
        {
            Random random = new Random();
            int randomId = random.Next(1, 48);
            return randomId;
        }
    }
}

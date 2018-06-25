using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack.ConnectionSettings
{
    public static class ConnectionStrings
    {
        public static string Connection { get; } =
            "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=BlackJaclDB;Integrated Security=True";

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using BlackJack.DAL.Dapper;
using BlackJack.DAL.Entities;

namespace BlackJack.DAL.Repositories
{
    public class RoundRepository:DpGenericRepository<Round>
    {
        public RoundRepository( ) : base()
        {
        }
    }
}

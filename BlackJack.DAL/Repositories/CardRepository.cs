using System;
using System.Collections.Generic;
using System.Text;
using BlackJack.DAL.Dapper;
using BlackJack.DAL.Entities;

namespace BlackJack.DAL.Repositories
{
    public class CardRepository:DpGenericRepository<Card>
    {
        public CardRepository():base()
        {
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using BlackJack.DAL.Dapper;
using Type = BlackJack.DAL.Entities.Type;

namespace BlackJack.DAL.Repositories
{
    public class TypeRepository:DpGenericRepository<Type>
    {
        public TypeRepository( ) : base()
        {
        }
    }
}

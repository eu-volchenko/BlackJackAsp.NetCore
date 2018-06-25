using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;

namespace BlackJack.DAL.Entities
{
    public class UserCard:BaseEntity
    {
        public int UserId { get; set; }

        public int CardId { get; set; }

        public int RoundId { get; set; }

        [Computed]
        public virtual Card Card { get; set; }

        [Computed]
        public virtual Round Round { get; set; }

        [Computed]
        public virtual User User { get; set; }
        [Computed]
        public virtual BaseEntity BaseEntity { get; set; }
    }
}

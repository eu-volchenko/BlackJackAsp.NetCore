﻿using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;

namespace BlackJack.DAL.Entities
{
    public class Round:BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Round()
        {
            UserCards = new HashSet<UserCard>();
        }

        public int UserId { get; set; }

        public int GameId { get; set; }

        public int RoundInGame { get; set; }

        [Computed]
        public virtual Game Game { get; set; }

        [Computed]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserCard> UserCards { get; set; }

        [Computed]
        public virtual User User { get; set; }

        [Computed]
        public virtual BaseEntity BaseEntity { get; set; }
    }
}

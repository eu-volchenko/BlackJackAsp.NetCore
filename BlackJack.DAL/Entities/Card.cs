using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Dapper.Contrib.Extensions;


namespace BlackJack.DAL.Entities
{
    public class Card:BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Card() => UserCards = new HashSet<UserCard>();

        [Required]
        public string Amount { get; set; }

        [Required]
        public string Suit { get; set; }

        public int Cost { get; set; }

        [Computed]
        public virtual BaseEntity BaseEntity { get; set; }

        [Computed]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserCard> UserCards { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Dapper.Contrib.Extensions;

namespace BlackJack.DAL.Entities
{
    public class Type:BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Type()
        {
            Users = new HashSet<User>();
        }

        [Required]
        public string Name { get; set; }

        [Computed]
        public virtual BaseEntity BaseEntity { get; set; }

        [Computed]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
    }
}

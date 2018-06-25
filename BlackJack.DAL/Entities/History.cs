using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Dapper.Contrib.Extensions;

namespace BlackJack.DAL.Entities
{
    [System.ComponentModel.DataAnnotations.Schema.Table("Histories")]
    public  class History : BaseEntity
    {
        public DateTime LogDateTime { get; set; }

        public int GameId { get; set; }

        [Computed]
        public virtual Game Game { get; set; }

        [Computed]
        public virtual BaseEntity BaseEntity { get; set; }
    }
}

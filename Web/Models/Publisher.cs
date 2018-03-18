namespace Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Publisher")]
    public partial class Publisher
    { 

        [Key]
        public long PubId { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public virtual IEnumerable<Book> Books { get; set; }

    }
}

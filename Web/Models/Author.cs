namespace Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Author")]
    public partial class Author
    { 
        public long AuthorId { get; set; }

        [StringLength(250)]
        public string AuthorName { get; set; }

        [StringLength(250)]
        public string History { get; set; }

        public virtual IEnumerable<Book> Books { get; set; }

    }
}

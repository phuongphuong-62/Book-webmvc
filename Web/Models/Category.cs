namespace Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        [Key]
        public long CateId { get; set; }

        [StringLength(250)]
        public string CateName { get; set; }

        [StringLength(500)]
        public string Desciption { get; set; }
        public virtual IEnumerable<Book> Books { get; set; }
    }
}

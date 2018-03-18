namespace Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comment")]
    public partial class Comment
    {
        [Key]
        public long Commentld { get; set; }

        public long? BookId { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool? IsActive { get; set; }

        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }

    }
}

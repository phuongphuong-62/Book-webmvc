namespace Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("Book")]
    public partial class Book
    { 

        public long BookId { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        public long? CateId { get; set; }

        public long? AuthorId { get; set; }

        public long? PubId { get; set; }

        [StringLength(250)]
        public string Summary { get; set; }

        [StringLength(250)]
        public string ImgUrl { get; set; }

        public decimal? Price { get; set; }

        public int? Quantity { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public long? ViewCount { get; set; }

        public bool IsActive { get; set; } 

        [ForeignKey("CateId")]
        public virtual Category Categories { set; get; }
        [ForeignKey("AuthorId")]
        public virtual Author Authors { set; get; }
        [ForeignKey("PubId")]
        public virtual Publisher Publishers { set; get; }


        public virtual IEnumerable<Comment> Comments { get; set; }
    }
}

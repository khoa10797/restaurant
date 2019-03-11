namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
        }

        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(20)]
        public string categoryID { get; set; }

        [StringLength(100)]
        public string productName { get; set; }

        [Column(TypeName = "ntext")]
        public string picture { get; set; }

        public double? price { get; set; }

        [Column(TypeName = "ntext")]
        public string note { get; set; }

        public int? buyCount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
    }
}

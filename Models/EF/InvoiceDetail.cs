namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class InvoiceDetail
    {
        public int id { get; set; }

        [Required]
        [StringLength(20)]
        public string tableID { get; set; }

        [Required]
        [StringLength(50)]
        public string invoiceID { get; set; }

        [Required]
        [StringLength(50)]
        public string productID { get; set; }

        public byte? quantity { get; set; }

        public bool? status { get; set; }

        public virtual Invoice Invoice { get; set; }

        public virtual Product Product { get; set; }

        public virtual Table Table { get; set; }
    }
}

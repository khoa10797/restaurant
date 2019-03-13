namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [StringLength(20)]
        public string id { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [StringLength(20)]
        public string cmnd { get; set; }

        [Column(TypeName = "date")]
        public DateTime? birthday { get; set; }

        public bool? sex { get; set; }

        [StringLength(50)]
        public string address { get; set; }

        [StringLength(15)]
        public string phoneNumber { get; set; }

        [StringLength(255)]
        public string accountName { get; set; }

        [StringLength(65)]
        public string accountPassword { get; set; }

        [StringLength(20)]
        public string groupID { get; set; }

        public virtual GroupUser GroupUser { get; set; }
    }
}

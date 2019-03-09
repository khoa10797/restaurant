namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Roles = new HashSet<Role>();
        }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Role> Roles { get; set; }
    }
}

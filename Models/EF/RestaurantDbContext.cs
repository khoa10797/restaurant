namespace Models.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext()
            : base("name=RestaurantDbContext")
        {
        }

        public virtual DbSet<GroupUser> GroupUsers { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Table> Tables { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupUser>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.GroupUser)
                .HasForeignKey(e => e.groupID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<GroupUser>()
                .HasMany(e => e.Roles)
                .WithMany(e => e.GroupUsers)
                .Map(m => m.ToTable("Credential").MapLeftKey("groupID").MapRightKey("roleID"));

            modelBuilder.Entity<Invoice>()
                .HasMany(e => e.Tables)
                .WithMany(e => e.Invoices)
                .Map(m => m.ToTable("InvoiceHasTable").MapLeftKey("invoiceID").MapRightKey("tableID"));

            modelBuilder.Entity<ProductCategory>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.ProductCategory)
                .HasForeignKey(e => e.categoryID);
        }
    }
}

namespace Web1.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EShopV20 : DbContext
    {
        public EShopV20()
            : base("name=EShopV20")
        {
        }
    
        public DbSet<ActionRole> ActionRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<MasterRole> MasterRoles { get; set; }
        public DbSet<Master> Masters { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<WebAction> WebActions { get; set; }
    }
}

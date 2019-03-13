using Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class ProductDAO
    {
        private RestaurantDbContext dbContext = new RestaurantDbContext();

        public string Add(Product product)
        {
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            return product.id;
        }

        public string Remove(string productId)
        {
            Product product = new Product();
            product.id = productId;
            dbContext.Entry(product).State = EntityState.Deleted;
            dbContext.SaveChanges();
            return productId;
        }

        public IQueryable<Product> GetAllProduct()
        {
            return dbContext.Products.AsQueryable();
        }

        public IQueryable<Product> GetByCategoryID(string categoryID)
        {
            return dbContext.Products.AsQueryable().Where(product => product.categoryID.Equals(categoryID));
        }

        public string GetNameByID(string id)
        {
            string name = dbContext.Products.First(product => product.id == id).productName;
            return name;
        }

        public Product GetByID(string id)
        {
            return dbContext.Products.SingleOrDefault(product => product.id.Equals(id));
        }

        public void UpdateBuyCount(string id, int quantity)
        {
            var product = dbContext.Products.First(item => item.id == id);
            product.buyCount += quantity;
            dbContext.SaveChanges();
        }
    }
}

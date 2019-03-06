using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class ProductDAO
    {
        private RestaurantDbContext dbContext = new RestaurantDbContext();

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
    }
}

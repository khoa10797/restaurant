using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class TableDAO
    {
        private RestaurantDbContext dbContext = new RestaurantDbContext();

        public IQueryable<Table> GetAllTable()
        {
            return dbContext.Tables.AsQueryable();
        }

        public void ChangeStatus(string tableID)
        {
            var table = dbContext.Tables.First(item => item.id == tableID);
            table.status = !table.status;
            dbContext.SaveChanges();
        }
    }
}

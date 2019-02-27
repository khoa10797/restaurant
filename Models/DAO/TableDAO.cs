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

        public List<Table> GetAllTableByInvoiceId(string invoiceId)
        {
            return dbContext.Invoices.Where(invoice => invoice.id == invoiceId).First().Tables.ToList();
        }

        public void AddTableForInvoice(string invoiceId, List<int> listTableId)
        {
            var table = dbContext.Invoices.First(invoice => invoice.id == invoiceId).Tables;

            listTableId.ForEach(tableId =>
            {
                var item = new Table()
                {
                    id = tableId.ToString()
                };
                dbContext.Tables.Attach(item);
                table.Add(item);
                dbContext.Tables.First(t => t.id == tableId.ToString()).status = true;
            });

            dbContext.SaveChanges();
        }

        public void ChangeStatus(string tableID)
        {
            var table = dbContext.Tables.First(item => item.id == tableID);
            table.status = !table.status;
            dbContext.SaveChanges();
        }
    }
}

using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Models.DAO
{
    public class InvoiceDAO
    {
        private RestaurantDbContext dbContext = new RestaurantDbContext();

        public string Add(Invoice invoice)
        {
            dbContext.Invoices.Add(invoice);
            dbContext.SaveChanges();
            return invoice.id;
        }

        public string Remove(Invoice invoice)
        {
            dbContext.Invoices.Remove(invoice);
            dbContext.SaveChanges();
            return invoice.id;
        }

        public Invoice GetInvoiceById(string id)
        {
            return dbContext.Invoices.First(invoice => invoice.id == id);
        }

        public Invoice GetLastInvoiceByTableId(string tableID)
        {
            return dbContext.Invoices.SqlQuery("SELECT * FROM Invoice WHERE id IN (SELECT invoiceID FROM InvoiceHasTable WHERE tableID = @id) AND status = 1", new SqlParameter("@id", tableID)).First();
        }

        public IQueryable<Invoice> GetAllUnpayInvoice()
        {
            return dbContext.Invoices.Where(invoice => invoice.status == true);
        }

        public void SetOffStatus(string invoiceID)
        {
            var invoice = dbContext.Invoices.First(item => item.id == invoiceID);
            invoice.status = false;
            dbContext.SaveChanges();
        }
    }
}

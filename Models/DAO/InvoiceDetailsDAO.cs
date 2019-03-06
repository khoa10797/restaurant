using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Models.DAO
{
    public class InvoiceDetailsDAO
    {
        private RestaurantDbContext dbContext = new RestaurantDbContext();

        public IQueryable<InvoiceDetail> GetAllByInvoiceAndTable(string invoiceID, string tableID)
        {
            return dbContext.InvoiceDetails
                .AsQueryable()
                .Where(item => item.invoiceID == invoiceID && item.tableID == tableID)
                .Include(x => x.Product)
                .Include(x => x.Invoice);
        }

        public IQueryable<InvoiceDetail> GetAllInvoiceDetailsWaiting()
        {
            return dbContext.InvoiceDetails.Where(item => item.status == true)
                .Include(x => x.Product)
                .Include(x => x.Invoice);
        }

        public InvoiceDetail FindById(string id)
        {
            return dbContext.InvoiceDetails.First(invoice => invoice.id == id);
        }

        public void SetStatusOff(string id)
        {
            var invoiceDetails = dbContext.InvoiceDetails.First(item => item.id == id);
            invoiceDetails.status = false;
            dbContext.SaveChanges();
        }

        public string Add(InvoiceDetail invoiceDetail)
        {
            dbContext.InvoiceDetails.Add(invoiceDetail);
            dbContext.SaveChanges();
            return invoiceDetail.id;
        }

        public void Add(List<InvoiceDetail> invoiceDetails)
        {
            invoiceDetails.ForEach(item =>
            {
                dbContext.InvoiceDetails.Add(item);
            });
            dbContext.SaveChanges();
        }

        public string Remove(string id)
        {
            var invoiceDetails = new InvoiceDetail();
            invoiceDetails.id = id;
            dbContext.Entry(invoiceDetails).State = EntityState.Deleted;
            dbContext.SaveChanges();
            return id;
        }

    }
}

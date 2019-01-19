using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Models.DAO;
using Models.EF;

namespace restaurant
{
    public class RestaurantHub : Hub
    {
        private InvoiceDetailsDAO invoiceDetailsDAO = new InvoiceDetailsDAO();
        private ProductDAO productDAO = new ProductDAO();

        public void AddNewOrder()
        {
            var listInvoiceWatting = invoiceDetailsDAO.GetAllInvoiceDetailsWaiting().Select(item => new { item.Invoice.tableID, item.Product.productName, item.quantity, item.id });
            Clients.All.addNewOrderForKitChen(listInvoiceWatting);
        }
    }
}
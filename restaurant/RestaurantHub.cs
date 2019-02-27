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

        public void AddNewOrder(string tableID, string invoiceID, string productID, byte quantity)
        {
            InvoiceDetail invoiceDetail = new InvoiceDetail()
            {
                id = Common.CreateKey.InvoiceDetails(tableID),
                tableID = tableID,
                invoiceID = invoiceID,
                productID = productID,
                quantity = quantity,
                status = true
            };
            invoiceDetailsDAO.Add(invoiceDetail);

            UpdateWaittingFoodForClients();
        }

        public void RemoveOrder(string invoiceDetailsId)
        {
            invoiceDetailsDAO.Remove(invoiceDetailsId);

            UpdateWaittingFoodForClients();
        }

        private void UpdateWaittingFoodForClients()
        {
            var listInvoiceWatting = invoiceDetailsDAO.GetAllInvoiceDetailsWaiting().Select(item => new
            {
                item.tableID,
                item.Product.productName,
                item.quantity,
                item.id
            });

            Clients.All.updateWaittingFood(listInvoiceWatting);
        }
    }
}
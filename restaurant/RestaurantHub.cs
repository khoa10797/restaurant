using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Models.DAO;
using Models.EF;
using restaurant.Models;

namespace restaurant
{
    public class RestaurantHub : Hub
    {
        private InvoiceDetailsDAO invoiceDetailsDAO = new InvoiceDetailsDAO();
        private InvoiceDAO invoiceDAO = new InvoiceDAO();

        public void AddNewOrder(string tableID, List<Cart> listCart)
        {
            List<InvoiceDetail> invoiceDetails = new List<InvoiceDetail>();
            listCart.ForEach(cart =>
            {
                InvoiceDetail item = new InvoiceDetail()
                {
                    tableID = tableID,
                    invoiceID = invoiceDAO.GetLastInvoiceByTableId(tableID).id,
                    productID = cart.productID,
                    quantity = Convert.ToByte(cart.quantity),
                    status = true
                };
                invoiceDetails.Add(item);
            });

            invoiceDetailsDAO.Add(invoiceDetails);

            UpdateWaittingFoodForClients();
        }

        public void RemoveOrder(int invoiceDetailsId)
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
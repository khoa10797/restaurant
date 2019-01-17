using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace restaurant.Controllers
{
    public class OrderController : Controller
    {

        private InvoiceDAO invoiceDAO = new InvoiceDAO();
        private InvoiceDetailsDAO invoiceDetailsDAO = new InvoiceDetailsDAO();
        private ProductDAO productDAO = new ProductDAO();
        private TableDAO tableDAO = new TableDAO();


        public ActionResult Index(string tableID = "101")
        {
            SetViewBag(tableID);
            return View("Index");
        }

        [HttpPost]
        public ActionResult CreateInvoiceDetails(string tableID, string invoiceID, string productID, byte quantity)
        {
            InvoiceDetail invoiceDetail = new InvoiceDetail()
            {
                id = Common.CreateKey.InvoiceDetails(tableID),
                invoiceID = invoiceID,
                productID = productID,
                quantity = quantity,
                status = true
            };
            invoiceDetailsDAO.Add(invoiceDetail);
            SetViewBag(tableID);
            return View("Index");
        }

        public ActionResult DeleteInvoiceDetails(string invoiceDetailsID, string tableID)
        {
            invoiceDetailsDAO.Remove(invoiceDetailsID);
            SetViewBag(tableID);
            return View("Index");
        }

        private void SetViewBag(string tableID)
        {
            var invoiceID = invoiceDAO.GetLastInvoiceByTableId(tableID).id;

            ViewBag.InvoiceID = invoiceID;
            ViewBag.ListTable = new SelectList(tableDAO.GetAllTable().Where(table => table.status == true), "id", "id");
            ViewBag.ListProduct = new SelectList(productDAO.GetAllProduct(), "id", "productName");
            ViewBag.ListOrder = invoiceDetailsDAO.GetAllByInvoiceID(invoiceID).Where(x => x.status == true);
            ViewBag.ListInvoiceDetail = invoiceDetailsDAO.GetAllByInvoiceID(invoiceID).Where(x => x.status == false);
        }
    }
}
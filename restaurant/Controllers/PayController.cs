using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace restaurant.Controllers
{
    public class PayController : Controller
    {
        private InvoiceDAO invoiceDAO = new InvoiceDAO();
        private InvoiceDetailsDAO invoiceDetailsDAO = new InvoiceDetailsDAO();

        // GET: Pay
        public ActionResult Index()
        {
            ViewBag.ListInvoice = invoiceDAO.GetAllUnpayInvoice();
            return View();
        }

        public ActionResult PayDetails(string invoiceID)
        {
            SetViewBagForDetails(invoiceID);
            return View("Details");
        }

        private void SetViewBagForDetails(string invoiceID)
        {
            ViewBag.Invoice = invoiceDAO.GetInvoiceById(invoiceID);
            ViewBag.ListInvoiceDetails = invoiceDetailsDAO.GetAllByInvoiceID(invoiceID).Where(item => item.status == false);
        }
    }
}
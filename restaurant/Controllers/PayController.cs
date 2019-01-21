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
        private TableDAO tableDAO = new TableDAO();

        // GET: Pay
        public ActionResult Index()
        {
            ViewBag.ListInvoice = invoiceDAO.GetAllUnpayInvoice();
            return View();
        }

        public ActionResult PayDetails(string invoiceID)
        {
            ViewBag.Invoice = invoiceDAO.GetInvoiceById(invoiceID);
            ViewBag.ListInvoiceDetails = invoiceDetailsDAO.GetAllByInvoiceID(invoiceID).Where(item => item.status == false);
            return View("Details");
        }

        public ActionResult PayInvoice(string invoiceID, string tableID)
        {
            tableDAO.ChangeStatus(tableID);
            invoiceDAO.SetOffStatus(invoiceID);
            return RedirectToAction("Index");
        }
    }
}
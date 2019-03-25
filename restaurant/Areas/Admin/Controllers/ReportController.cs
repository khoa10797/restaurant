using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace restaurant.Areas.Admin.Controllers
{
    public class ReportController : Controller
    {
        InvoiceDAO invoiceDAO = new InvoiceDAO();
        InvoiceDetailsDAO invoiceDetailsDAO = new InvoiceDetailsDAO();

        // GET: Admin/Report
        public ActionResult Revenue()
        {
            return View();
        }

        public ActionResult FindInvoice(DateTime? date)
        {
            List<Invoice> model;
            if (!date.HasValue)
                model = invoiceDAO.GetAllInvoice().ToList();
            else
                model = invoiceDAO.GetAllByDate(date.Value).ToList();
            return View(model);
        }

        public ActionResult GetInovoiceDetails(string invoiceID)
        {
            var model = invoiceDetailsDAO.GetByInvoiceID(invoiceID).ToList();
            return View("InvoiceDetail", model);
        }
    }
}
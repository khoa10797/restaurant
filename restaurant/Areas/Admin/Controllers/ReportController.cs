using Models.DAO;
using Models.EF;
using restaurant.Models;
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
        [Credential(roleID = "VIEW_REPORT")]
        public ActionResult Revenue(int? year)
        {
            if (year == null)
                year = DateTime.Now.Year;
            List<double> prices = new List<double>();
            for (int i = 1; i <= 12; i++)
                prices.Add(invoiceDAO.FindRevenueByMonth(i, year));
            ViewBag.Revenue = prices;
            return View();
        }

        [Credential(roleID = "VIEW_REPORT")]
        public ActionResult FindInvoice(DateTime? date)
        {
            List<Invoice> model;
            if (!date.HasValue)
                model = invoiceDAO.GetAllInvoice().ToList();
            else
                model = invoiceDAO.GetByDate(date.Value).ToList();
            return View(model);
        }

        [Credential(roleID = "VIEW_REPORT")]
        public ActionResult GetInovoiceDetails(string invoiceID)
        {
            var model = invoiceDetailsDAO.GetByInvoiceID(invoiceID).ToList();
            return View("InvoiceDetail", model);
        }
    }
}
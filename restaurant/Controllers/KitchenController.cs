using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace restaurant.Controllers
{
    public class KitchenController : Controller
    {

        private InvoiceDetailsDAO invoiceDetailsDAO = new InvoiceDetailsDAO();

        // GET: Kitchen
        public ActionResult Index()
        {
            SetViewBag();
            return View();
        }

        public ActionResult Complete(string invoiceDetailsID)
        {
            invoiceDetailsDAO.SetStatusOff(invoiceDetailsID);
            SetViewBag();
            return View("Index");
        }

        private void SetViewBag()
        {
            ViewBag.ListOrder = invoiceDetailsDAO.GetAllInvoiceDetailsWaiting();
        }
    }
}
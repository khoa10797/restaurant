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
        private ProductDAO productDAO = new ProductDAO();

        // GET: Kitchen
        public ActionResult Index()
        {
            SetViewBag();
            return View();
        }

        public ActionResult Complete(int invoiceDetailsID, string productId, int quantity)
        {
            invoiceDetailsDAO.SetStatusOff(invoiceDetailsID);
            productDAO.UpdateBuyCount(productId, quantity);
            return RedirectToAction("Index");
        }

        private void SetViewBag()
        {
            ViewBag.ListOrder = invoiceDetailsDAO.GetAllInvoiceDetailsWaiting();
        }
    }
}
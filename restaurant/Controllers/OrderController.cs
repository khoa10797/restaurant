using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using restaurant.Models;

namespace restaurant.Controllers
{
    public class OrderController : Controller
    {

        private InvoiceDAO invoiceDAO = new InvoiceDAO();
        private InvoiceDetailsDAO invoiceDetailsDAO = new InvoiceDetailsDAO();
        private ProductDAO productDAO = new ProductDAO();
        private TableDAO tableDAO = new TableDAO();

        [Credential(roleID = "VIEW_INVOICE_D")]
        public ActionResult Index(string tableID)
        {
            if (tableID == null)
                tableID = tableDAO.GetTableHasPeople().ToList()[0].id;
            SetViewBag(tableID);
            return View("Index");
        }

        private void SetViewBag(string tableID)
        {
            var invoiceID = invoiceDAO.GetLastInvoiceByTableId(tableID).id;
            ViewBag.TableID = tableID;
            ViewBag.InvoiceID = invoiceID;
            ViewBag.ListTable = new SelectList(tableDAO.GetAllTable().Where(table => table.status == true), "id", "id", tableID);
            ViewBag.ListOrder = invoiceDetailsDAO.GetByInvoiceAndTable(invoiceID, tableID).Where(x => x.status == true).ToList();
            ViewBag.ListInvoiceDetail = invoiceDetailsDAO.GetByInvoiceAndTable(invoiceID, tableID).Where(x => x.status == false);
        }
    }
}
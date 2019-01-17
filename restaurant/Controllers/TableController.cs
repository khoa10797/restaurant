using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Models.EF;

namespace restaurant.Controllers
{
    public class TableController : Controller
    {
        private TableDAO tableDAO = new TableDAO();
        private InvoiceDAO invoiceDAO = new InvoiceDAO();

        // GET: Table
        public ActionResult Index(int? page)
        {
            var tables = tableDAO.GetAllTable();
            int pageSize = 24;
            int pageNumber = (page ?? 1);
            return View(tables.OrderBy(table => table.id).ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult AddInvoice(string tableId, string customerName, string customerPhone)
        {
            Invoice invoice = new Invoice()
            {
                id = Common.CreateKey.Invoice(tableId),
                tableID = tableId,
                customerName = customerName,
                customerPhone = customerPhone,
                status = true
            };
            invoiceDAO.Add(invoice);
            tableDAO.ChangeStatus(tableId);
            return RedirectToAction("Index");
        }
    }
}
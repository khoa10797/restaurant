using Models.DAO;
using Models.EF;
using restaurant.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace restaurant.Controllers
{
    public class InvoiceController : Controller
    {
        private InvoiceDAO invoiceDAO = new InvoiceDAO();
        private InvoiceDetailsDAO invoiceDetailsDAO = new InvoiceDetailsDAO();
        private TableDAO tableDAO = new TableDAO();

        // GET: Pay
        [Credential(roleID = "VIEW_INVOICE")]
        public ActionResult Index()
        {
            ViewBag.ListInvoice = invoiceDAO.GetAllUnpayInvoice();
            return View();
        }

        public PartialViewResult GetPaging(int? page)
        {
            var tables = tableDAO.GetAllTable();
            int pageSize = 30;
            int pageNumber = (page ?? 1);
            return PartialView("_ListTable", tables.OrderBy(table => table.id).ToPagedList(pageNumber, pageSize));
        }

        [Credential(roleID = "VIEW_INVOICE")]
        public ActionResult PayDetails(string invoiceID)
        {
            List<Table> tables = tableDAO.GetAllTableByInvoiceId(invoiceID);
            List<InvoiceDetail> invoiceDetails = new List<InvoiceDetail>();

            tables.ForEach(table =>
            {
                invoiceDetailsDAO.GetAllByInvoiceAndTable(invoiceID, table.id).ToList().ForEach(invoiceDetail =>
                {
                    invoiceDetails.Add(invoiceDetail);
                });
            });

            ViewBag.Invoice = invoiceDAO.GetInvoiceById(invoiceID);
            ViewBag.ListInvoiceDetails = invoiceDetails;
            ViewBag.ListTable = tableDAO.GetAllTableByInvoiceId(invoiceID);
            return View("Details");
        }

        [Credential(roleID = "PAY_INVOICE")]
        public ActionResult PayInvoice(string invoiceID)
        {
            List<Table> tables = tableDAO.GetAllTableByInvoiceId(invoiceID);
            tables.ForEach(table =>
            {
                tableDAO.ChangeStatus(table.id);
            });
            invoiceDAO.SetOffStatus(invoiceID);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Credential(roleID = "CREATE_INVOICE")]
        public ActionResult AddInvoice(string customerName, string customerPhone)
        {
            Invoice invoice = new Invoice()
            {
                id = Common.CreateKey.Invoice(),
                customerName = customerName,
                customerPhone = customerPhone,
                status = true
            };
            invoiceDAO.Add(invoice);
            return Json(invoice, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void AddTableForInvoice(string invoiceId, List<int> listTable)
        {
            tableDAO.AddTableForInvoice(invoiceId, listTable);
        }
    }
}
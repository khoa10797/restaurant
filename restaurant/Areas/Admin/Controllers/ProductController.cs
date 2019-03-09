using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace restaurant.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        ProductDAO productDAO = new ProductDAO();

        // GET: Admin/Product
        public ActionResult Index()
        {
            return View();
        }
    }
}
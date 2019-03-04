using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace restaurant.Controllers
{
    public class MenuController : Controller
    {

        private ProductDAO productDAO = new ProductDAO();

        // GET: Menu
        public ActionResult Index(int? page,string categoryId = "01")
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var products = productDAO.GetByCategoryID(categoryId).ToList();

            ViewBag.CategoryId = categoryId;
            switch (Convert.ToInt32(categoryId))
            {
                case 1:
                    ViewBag.SubTitle = "Món khai vị";
                    break;
                case 2:
                    ViewBag.SubTitle = "Món chính";
                    break;
                default:
                    ViewBag.SubTitle = "Món tráng miệng";
                    break;
            }
            return View(products.OrderBy(product => product.buyCount).ToPagedList(pageNumber, pageSize));
        }
    }
}
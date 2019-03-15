using Models.DAO;
using restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace restaurant.Controllers
{
    public class CartController : Controller
    {
        ProductDAO productDAO = new ProductDAO();
        TableDAO tableDAO = new TableDAO();

        [Credential(roleID = "VIEW_CART")]
        public ActionResult Index()
        {
            List<Cart> carts = Session["cart"] as List<Cart>;
            ViewBag.ListCart = carts;
            return View();
        }

        public PartialViewResult GetPaging(int? page)
        {
            var tables = tableDAO.GetAllTable().Where(table => table.status == true);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return PartialView("_ListTableCart", tables.OrderBy(table => table.id).ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        [Credential(roleID = "ADD_CART")]
        public ActionResult AddCart(string productId)
        {
            if (Session["cart"] == null)
            {
                Session["cart"] = new List<Cart>();
            }

            var carts = Session["cart"] as List<Cart>;

            var name = carts.Find(item => item.productID == productId);
            if (name == null)
            {
                string productName = productDAO.GetNameByID(productId);
                Cart cart = new Cart(productId, productName, 1);
                carts.Add(cart);
            }

            return Json("true", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Credential(roleID = "REMOVE_CART")]
        public ActionResult RemoveCart(string productId)
        {
            List<Cart> carts = Session["cart"] as List<Cart>;
            carts.Remove(carts.Find((cart => cart.productID == productId)));
            return Json("true", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Credential(roleID = "UPDATE_CART")]
        public ActionResult UpdateCart(string productId, int quantity)
        {
            List<Cart> carts = Session["cart"] as List<Cart>;
            var item = carts.Find(cart => cart.productID == productId);
            item.quantity = quantity;
            return Json("true", JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveSession()
        {
            Session.Remove("cart");
            return RedirectToAction("Index", "Gallery");
        }
    }
}
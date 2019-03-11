using Models.DAO;
using Models.EF;
using restaurant.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace restaurant.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        ProductDAO productDAO = new ProductDAO();

        // GET: Admin/Product
        public ActionResult Index(int? page)
        {
            var product = productDAO.GetAllProduct();
            int pageSize = 30;
            int pageNumber = (page ?? 1);
            return View(product.OrderBy(item => item.id).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(ProductUpload upload)
        {
            try
            {
                HttpPostedFileBase file = upload.imageFile;
                if (file.ContentLength > 0)
                {
                    Product product = GetProductFromUpload(upload);
                    productDAO.Add(product);
                    string path = Server.MapPath("~/images/item/") + file.FileName;
                    file.SaveAs(path);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Thêm sản phẩm thất bại!";
                    return View("Error");
                }
            }
            catch
            {
                ViewBag.Error = "Thêm sản phẩm thất bại!";
                return View("Error");
            }
        }

        private Product GetProductFromUpload(ProductUpload upload)
        {
            Product product = new Product()
            {
                id = upload.id,
                categoryID = upload.categoryID,
                productName = upload.productName,
                picture = "/images/item/" + upload.imageFile.FileName,
                price = upload.price,
                note = upload.note,
                buyCount = 0
            };

            return product;
        }

        [HttpPost]
        public ActionResult RemoveProduct(string id)
        {
            productDAO.Remove(id);
            return Json("true", JsonRequestBehavior.AllowGet);
        }
    }
}
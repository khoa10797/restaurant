﻿using Models.DAO;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace restaurant.Controllers
{
    public class GalleryController : Controller
    {
        private ProductDAO productDAO = new ProductDAO();

        // GET: Gallery
        public ActionResult Index(int? page, string categoryId = "01")
        {
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            var products = productDAO.GetByCategoryID(categoryId).ToList();

            ViewBag.CategoryId = categoryId;
            return View(products.OrderBy(product => product.buyCount).ToPagedList(pageNumber, pageSize));
        }
    }
}
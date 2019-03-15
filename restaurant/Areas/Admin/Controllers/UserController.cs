using Models.DAO;
using Models.EF;
using PagedList;
using restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace restaurant.Areas.Admin.Controllers
{
    public class UserController : Controller
    {

        private UserDAO userDAO = new UserDAO();

        // GET: Admin/User
        [Credential(roleID = "VIEW_USER")]
        public ActionResult Index(int? page)
        {
            var users = userDAO.GetAllUsers();
            int pageSize = 30;
            int pageNumber = (page ?? 1);

            return View(users.OrderBy(user => user.id).ToPagedList(pageNumber, pageSize));
        }

        [Credential(roleID = "ADD_USER")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Credential(roleID = "ADD_USER")]
        public ActionResult AddUser(User user)
        {
            userDAO.Add(user);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Credential(roleID = "REMOVE_USER")]
        public ActionResult RemoveUser(string id)
        {
            userDAO.Remove(id);
            return Json("true", JsonRequestBehavior.AllowGet);
        }
    }
}
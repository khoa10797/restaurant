using Models.DAO;
using Models.EF;
using restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace restaurant.Controllers
{
    public class LoginController : Controller
    {
        private UserDAO userDAO = new UserDAO();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckLogin(string userName, string password)
        {
            bool test = userDAO.CheckLogin(userName, password);
            if (test)
            {
                User user = userDAO.GetByUserName(userName);
                List<string> role = userDAO.GetRoleByID(user.id);
                Session.Add("role", role);
                Session["user"] = user;
                return RedirectToAction("Index", "Kitchen");
            }
            return View("Index");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}
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
            if (Request.Cookies["username"] != null && Request.Cookies["password"] != null)
            {
                var username = Request.Cookies["username"].Value.ToString();
                var password = Request.Cookies["password"].Value.ToString();
                bool test = userDAO.CheckLogin(username, password);

                if (test)
                {
                    User user = userDAO.GetByUserName(username);
                    List<string> role = userDAO.GetRoleByID(user.id);
                    Session.Add("role", role);
                    Session["user"] = user;

                    return RedirectToAction("Index", "Kitchen");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult CheckLogin(string userName, string password, string remember)
        {
            bool test = userDAO.CheckLogin(userName, password);
            if (test)
            {
                User user = userDAO.GetByUserName(userName);
                List<string> role = userDAO.GetRoleByID(user.id);
                Session.Add("role", role);
                Session["user"] = user;

                if (remember == "true")
                {
                    HttpCookie cookieUserName = new HttpCookie("username", userName);
                    HttpCookie cookiePassword = new HttpCookie("password", password);
                    cookieUserName.Expires = DateTime.Now.AddDays(1);
                    cookiePassword.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(cookieUserName);
                    Response.Cookies.Add(cookiePassword);
                }

                return RedirectToAction("Index", "Kitchen");
            }
            ViewBag.Error = "Thông tin tài khoản hoặc mật khẩu không chính xác!";
            return View("Index");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            if (Response.Cookies["username"] != null)
            {
                Response.Cookies["username"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["password"].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("Index");
        }
    }
}
using Models.DAO;
using restaurant.Common;
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
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginModels models)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var res = dao.Login(models.userName, models.passWord);
                if (res)
                {
                    var user = dao.GetByUserName(models.userName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.accountName;
                    userSession.UserID = user.id;
                    Session.Add(CommonConst.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else
                {

                }
            }
            return View("Index");
        }
    }
}
using NIMAP2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NIMAP2.Controllers
{
    public class LoginController : Controller
    {

        ProductContext db = new ProductContext();

        // GET: Login
        public ActionResult SignUP()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUP(SignUp sp)
        {
            SignUp s = new SignUp();
            s.UserName = sp.UserName;
            s.UserEmail = sp.UserEmail;
            s.Password = sp.Password;
            s.ConfirmPassword = sp.ConfirmPassword;
            db.SignupTbl.Add(s);
            db.SaveChanges();
            return View("Login");
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(SignUp sp)
        {
            var s = db.SignupTbl.FirstOrDefault(model => model.UserName == sp.UserName && model.Password == sp.Password);
            if (s != null)
            {
                var token = TokenCreate.JwtCreateToken(s);

                Response.Cookies.Set(new HttpCookie("Bearer", token));

                return RedirectToAction("Index", "Home");

            }
            else
            {
                ViewBag.Message = "InValid Email or Password";
            }
            return View();
        }

        public ActionResult Logout()
        {
            var cookie = Request.Cookies["Bearer"];
            cookie.Expires = DateTime.Now.AddMinutes(2);
            Response.Cookies.Add(cookie);
            return RedirectToAction("Login");
        }
    }
}
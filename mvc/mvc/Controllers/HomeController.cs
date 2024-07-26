using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult HomePage()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact(FormCollection form)
        {
            ViewBag.Email = form["Email"];
            ViewBag.Age = form["age"];
            ViewBag.Gender = form["gender"];
            ViewBag.Courses = form["Courses"];
            ViewBag.Qualification = form["Select"];
          
            return View();
        }

        public ActionResult Login(FormCollection form)
        {
            ViewBag.Email = form["email"];
            ViewBag.Password = form["psw"];

            return View();
        }

        [HttpPost]
        public ActionResult Sell(FormCollection form)
        {
            ViewBag.Email = form["Email"];
            ViewBag.Age = form["age"];
            ViewBag.Gender = form["gender"];
            ViewBag.Courses = form["Courses"];
            ViewBag.Qualification = form["Select"];
            return View();
        }
        public ActionResult Display()
        {
            {
                return View();
            }
        }
    }
}

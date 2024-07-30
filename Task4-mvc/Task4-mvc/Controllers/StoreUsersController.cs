using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Task4_mvc.Models;

namespace Task4_mvc.Controllers
{
    public class StoreUsersController : Controller
    {
        private WardrobStoreEntities db = new WardrobStoreEntities();

        // GET: StoreUsers
        public ActionResult Index()
        {
            return View(db.StoreUsers.ToList());
        }





        public ActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Register(StoreUser storeUser,string confirmpassword)
        {
            if(storeUser.Password == confirmpassword) {

            db.StoreUsers.Add(storeUser);
            db.SaveChanges();
                return RedirectToAction("Index");
 }

            return View();
        }



        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(StoreUser storeUser)
        {
            var user = db.StoreUsers.FirstOrDefault(u => u.Email == storeUser.Email);
            if(user== null || user.Password != storeUser.Password) { 
                return View();}
            HttpCookie cookie = new HttpCookie("UserInfo");
            cookie["isLoogedIn"] = "true";
            cookie["userId"] = Convert.ToString(user.ID);
            Response.Cookies.Add(cookie);
            return RedirectToAction("Profile");


        }

        public ActionResult Profile()
        {
            HttpCookie cookie = Request.Cookies["UserInfo"];
            if (cookie["isLoogedIn"] != "true")
                return RedirectToAction("Index", "Home");
            StoreUser user = db.StoreUsers.Find(Convert.ToInt32(cookie["userId"]));

            return View(user);
            

        }


        [HttpPost]
        public ActionResult Profile(StoreUser users, HttpPostedFileBase upload)
        {


            if (upload != null && upload.ContentLength > 0)
            {
                var fileName = Path.GetFileName(upload.FileName);
                var path = Path.Combine(Server.MapPath("~/Images/"), fileName);

                if (!Directory.Exists(Server.MapPath("~/Images/")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Images/"));
                }

                upload.SaveAs(path);
                users.Img = fileName;


            }

            db.Entry(users).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Profile");

            

        }


        public ActionResult resetPassword()
        {

            return View();
        }

        [HttpPost]
        public ActionResult resetPassword (StoreUser storeUser,string oldPass,string ConfirmPass)
        {
            HttpCookie cookie = Request.Cookies["UserInfo"];
            StoreUser users = db.StoreUsers.Find(Convert.ToInt32(cookie["userId"]));
      

            if (users.Password == oldPass)
            {
                if (storeUser.Password == ConfirmPass)
                {
                    users.Password = storeUser.Password;
                    db.Entry(users).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            HttpCookie cookie = Request.Cookies["UserInfo"];
           
            
                if (cookie != null)
                {
                    // Set the cookie to expire immediately
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(cookie);
                }

                // Redirect to the Index action
                return RedirectToAction("Index");
            


       


        }


















        // GET: StoreUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreUser storeUser = db.StoreUsers.Find(id);
            if (storeUser == null)
            {
                return HttpNotFound();
            }
            return View(storeUser);
        }

        // GET: StoreUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StoreUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Email,Password,Img")] StoreUser storeUser)
        {
            if (ModelState.IsValid)
            {
                db.StoreUsers.Add(storeUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(storeUser);
        }

        // GET: StoreUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreUser storeUser = db.StoreUsers.Find(id);
            if (storeUser == null)
            {
                return HttpNotFound();
            }
            return View(storeUser);
        }

        // POST: StoreUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Email,Password,Img")] StoreUser storeUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(storeUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(storeUser);
        }

        // GET: StoreUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreUser storeUser = db.StoreUsers.Find(id);
            if (storeUser == null)
            {
                return HttpNotFound();
            }
            return View(storeUser);
        }

        // POST: StoreUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StoreUser storeUser = db.StoreUsers.Find(id);
            db.StoreUsers.Remove(storeUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

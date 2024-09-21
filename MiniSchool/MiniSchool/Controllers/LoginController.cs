using MiniSchool.Models;
using System.Linq;
using System.Web.Mvc;

namespace MiniSchool.Controllers
{
    public class LoginController : Controller
    {
        private readonly MiniSchoolContext _context = new MiniSchoolContext();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public ActionResult Login(User userModel)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users
                    .FirstOrDefault(u => u.Email == userModel.Email && u.Password == userModel.Password);

                if (user != null)
                {
                    // Store user info in session
                    Session["UserId"] = user.Id;
                    Session["UserEmail"] = user.Email;

                    // Redirect to a secure page
                    return RedirectToAction("Index", "MiniShcool");
                }

                ModelState.AddModelError("", "Invalid email or password.");
            }

            return View(userModel);
        }

        // GET: Logout
        public ActionResult Logout()
        {
            // Clear the session
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login", "Login");
        }
    }
}

using Microsoft.AspNet.Identity.Owin;
using MVC.Models;
using MVC.Models.Auth;
using MVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class RegisterController : Controller
    {
        private UserManager _authManager;

        public UserManager AuthManager
        {
            get
            {
                return _authManager ?? HttpContext.GetOwinContext().Get<UserManager>();
            }
            private set
            {
                _authManager = value;
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var usernameExists = await AuthManager.FindByNameAsync(model.UserName);

            if (usernameExists != null)
            {
                ModelState.AddModelError("", "Molim unesite drugo korisničko ime");
                return View(model);
            }

            var emailExists = await AuthManager.FindByEmailAsync(model.Email);

            if (emailExists != null)
            {
                ModelState.AddModelError("", "Molim unesite drugu email adresu");
                return View(model);

            }

            User newUser = new User(model.UserName, Processes.SHA512(model.PasswordHash), model.Email, model.PhoneNumber, model.Address);

            Repo.AddUser(model.UserName, Processes.SHA512(model.PasswordHash), model.Email, model.PhoneNumber, model.Address);
            await AuthManager.CreateAsync(newUser);
            return RedirectToAction("Index", "Login");
        }
    }
}
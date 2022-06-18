using MVC.Models.Auth;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MVC.Models;
using MVC.Models.CustomAttributes;
using MVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace MVC.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        private UserManager _authManager;
        private SignInManager _signInManager;

        public SignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<SignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

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
        [AllowAnonymous]
        [IsAuthorized]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await AuthManager.FindAsync(model.UserName, model.PasswordHash);
            if (user != null) 
            {
                await SignInManager.SignInAsync(user, true, model.RememberMe);
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ModelState.AddModelError("", "Korisnik ne postoji u bazi");
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Login");
        }
    }
}
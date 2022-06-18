using MVC.Models.Auth;
using Microsoft.AspNet.Identity.Owin;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    [Authorize]
    public class DashboardController : Controller
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

        public async Task<ActionResult> Index()
        {
            var username = User.Identity.Name;
            User model = await AuthManager.FindByNameAsync(username);
            return View(model);
        }
    }
}
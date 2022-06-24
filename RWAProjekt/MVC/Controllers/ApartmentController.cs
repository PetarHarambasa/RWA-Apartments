using MVC.Models.Auth;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    [Authorize]
    public class ApartmentController : Controller
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
            return View(Repo.LoadFreeApartments());
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            ViewBag.Apartman = Repo.LoadApartment(id);
            ViewBag.Tags = Repo.LoadApartmentTags(id);
            ViewBag.ApartmentPicture = Repo.LoadApartmentPicture(id);
            var username = User.Identity.Name;
            User model = await AuthManager.FindByNameAsync(username);
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult GetNameOwner(int id)
        {
            return Content(Repo.GetOwner(id).Name);
        }


        [ChildActionOnly]
        public ActionResult GetNameStatus(int id)
        {
            return Content(Repo.GetStatus(id).Name);
        }


        [ChildActionOnly]
        public ActionResult GetNameCity(int id)
        {
            return Content(Repo.GetCity(id).Name);
        }

        [ChildActionOnly]
        public ActionResult GetTagNameApartment(int id)
        {
            return Content(Repo.GetTag(id).Name);
        }

    }
}
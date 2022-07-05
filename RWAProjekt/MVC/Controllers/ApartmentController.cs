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

        public ActionResult Booked(string apartmentReservationDetails,int apartmentId, int userId)
        {
            Apartment apartment = Repo.LoadApartment(apartmentId);
            User u = Repo.LoadUser(userId);
            u.ApartmentReservationDetails = apartmentReservationDetails;
            ViewBag.User = u;
            Repo.AddApartmentReservation(apartmentId, u);
            return View(apartment);
        }

        [HttpPost]
        public void PostReview(int apartmentId, int userId, string details, int star)
        {
            ApartmentReview apartmentReview = new ApartmentReview();
            apartmentReview.ApartmentId = apartmentId;
            apartmentReview.UserId = userId;
            apartmentReview.Details = details;
            apartmentReview.RatingStars = star;
            Repo.AddApartmentReview(apartmentReview);
        }

        public ActionResult ThankYou()
        {
            return View();
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
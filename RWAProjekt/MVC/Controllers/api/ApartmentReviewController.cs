using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MVC.Controllers
{
    public class ApartmentReviewController : ApiController
    {
        // GET: api/ApartmentReview
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ApartmentReview/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ApartmentReview
        public void Post([FromBody]int apartmentId, int userId, string details)
        {
            ApartmentReview apartmentReview = new ApartmentReview();
            apartmentReview.ApartmentId = apartmentId;
            apartmentReview.UserId = userId;
            apartmentReview.Details = details;
            Repo.AddApartmentReview(apartmentReview);
        }

        // PUT: api/ApartmentReview/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApartmentReview/5
        public void Delete(int id)
        {
        }
    }
}

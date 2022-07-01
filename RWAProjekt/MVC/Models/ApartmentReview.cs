using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class ApartmentReview
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public int UserId { get; set; }
        public string Details { get; set; }
        public int RatingStars { get; set; }

        public ApartmentReview()
        {

        }
        public ApartmentReview(int apartmentId, int userId, string details, int ratingStars)
        {
            ApartmentId = apartmentId;
            UserId = userId;
            Details = details;
            RatingStars = ratingStars;
        }
    }
}
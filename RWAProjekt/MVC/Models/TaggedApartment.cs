using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class TaggedApartment
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public int TagId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rwaLib.Models
{
    public class TaggedApartment
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }

        public TaggedApartment()
        {

        }
        public TaggedApartment(int apartmentId, int tagId)
        {
            ApartmentId = apartmentId;
            TagId = tagId;
        }
    }
}

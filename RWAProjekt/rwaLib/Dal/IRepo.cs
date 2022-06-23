using rwaLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rwaLib.Dal
{
    public interface IRepo
    {
        IList<User> LoadUsers();
        void AddUser(User user);
        void SaveUser(User user);
        IList<ApartmentOwner> LoadApartmentOwner();
        IList<ApartmentStatus> LoadApartmentStatus();
        IList<City> LoadCity();
        IList<Apartment> LoadApartments();
        int AddApartment(Apartment apartment);
        void SaveApartment(Apartment apartment);
        void DeleteApartment(Apartment apartment);
        IList<Tag> LoadTags();
        IList<TagType> LoadTagTypes();
        void SaveTag(Tag tag);
        void DeleteTag(Tag tag);
        void AddTag(Tag tag);
        void AddApartmentTag(TaggedApartment tagged);
        void AddApartmentPicture(ApartmentPicture apartmentPicture);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rwaLib.Models
{
    [Serializable]
    public class Apartment
    {
        public int Id { get; set; }
        public DateTime DeletedAt { get; set; }
        public int OwnerId { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public int CityId { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public decimal Price { get; set; }
        public int MaxAdults { get; set; }
        public int MaxChildren { get; set; }
        public int TotalRooms { get; set; }
        public int BeachDistance { get; set; }

        public Apartment()
        {
        }

        public Apartment(int ownerId, int typeId, int statusId, int cityId, string address, string name, string nameEng, decimal price, int maxAdults, int maxChildren, int totalRooms, int beachDistance)
        {
            OwnerId = ownerId;
            TypeId = typeId;
            StatusId = statusId;
            CityId = cityId;
            Address = address;
            Name = name;
            NameEng = nameEng;
            Price = price;
            MaxAdults = maxAdults;
            MaxChildren = maxChildren;
            TotalRooms = totalRooms;
            BeachDistance = beachDistance;
        }

        public Apartment(string address, string name)
        {
            Address = address;
            Name = name;
        }
    }
}

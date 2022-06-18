using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public static class Repo
    {
        public static DataSet ds { get; set; }
        private static string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        public static List<TaggedApartment> LoadApartmentTags(int id)
        {
            List<TaggedApartment> apartmentTags = new List<TaggedApartment>();
            ds = SqlHelper.ExecuteDataset(cs, nameof(LoadApartmentTags), id);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var apartmentTag = new TaggedApartment();

                apartmentTag.Id = (int)row[nameof(TaggedApartment.Id)];
                apartmentTag.ApartmentId = (int)row[nameof(TaggedApartment.ApartmentId)];
                apartmentTag.TagId = (int)row[nameof(TaggedApartment.TagId)];
                apartmentTags.Add(apartmentTag);
            }

            return apartmentTags;
        }

        public static List<Apartment> LoadFreeApartments()
        {
            List<Apartment> apartments = new List<Apartment>();
            ds = SqlHelper.ExecuteDataset(cs, nameof(LoadFreeApartments));

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var apartment = new Apartment();

                apartment.Id = (int)row[nameof(Apartment.Id)];
                apartment.OwnerId = (row[nameof(Apartment.OwnerId)] != DBNull.Value ? (int)row[nameof(Apartment.OwnerId)] : 1);
                apartment.StatusId = (row[nameof(Apartment.StatusId)] != DBNull.Value ? (int)row[nameof(Apartment.StatusId)] : 1);
                apartment.CityId = (row[nameof(Apartment.CityId)] != DBNull.Value ? (int)row[nameof(Apartment.CityId)] : 1);
                apartment.Address = row[nameof(Apartment.Address)].ToString();
                apartment.Name = row[nameof(Apartment.Name)].ToString();
                apartment.NameEng = row[nameof(Apartment.NameEng)].ToString();
                apartment.Price = (decimal)row[nameof(Apartment.Price)];
                apartment.MaxAdults = (int)row[nameof(Apartment.MaxAdults)];
                apartment.MaxChildren = (int)row[nameof(Apartment.MaxChildren)];
                apartment.TotalRooms = (int)row[nameof(Apartment.TotalRooms)];
                apartment.BeachDistance = (int)row[nameof(Apartment.BeachDistance)];
                apartments.Add(apartment);
            }

            return apartments;
        }

        public static List<Tag> LoadTags()
        {
            List<Tag> tags = new List<Tag>();
            ds = SqlHelper.ExecuteDataset(cs, nameof(LoadTags));

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var tag = new Tag();

                tag.Id = (int)row[nameof(Tag.Id)];
                tag.Name = row[nameof(Tag.Name)].ToString();
                tag.NameEng = row[nameof(Tag.NameEng)].ToString();
                tags.Add(tag);
            }

            return tags;
        }

        public static Tag GetTag(int id) => LoadTags().FirstOrDefault(tag => tag.Id == id); 

        public static Apartment LoadApartment(int id)
        {
            ds = SqlHelper.ExecuteDataset(cs, nameof(LoadApartment), id);
            DataRow row = ds.Tables[0].Rows[0];

            Apartment a = new Apartment();
            a.Id = id;
            a.OwnerId = (int)row[nameof(Apartment.OwnerId)];
            a.StatusId = (int)row[nameof(Apartment.StatusId)];
            a.CityId = (int)row[nameof(Apartment.CityId)];
            a.Address = row[nameof(Apartment.Address)].ToString();
            a.Name = row[nameof(Apartment.Name)].ToString();
            a.NameEng = row[nameof(Apartment.NameEng)].ToString();
            a.Price = (decimal)row[nameof(Apartment.Price)];
            a.MaxAdults = (int)row[nameof(Apartment.MaxAdults)];
            a.MaxChildren = (int)row[nameof(Apartment.MaxAdults)];
            a.TotalRooms = (int)row[nameof(Apartment.TotalRooms)];
            a.BeachDistance = (int)row[nameof(Apartment.BeachDistance)];

            return a;
        }
        

        public static List<Owner> LoadApartmentOwner()
        {
            List<Owner> owners = new List<Owner>();
            ds = SqlHelper.ExecuteDataset(cs, nameof(LoadApartmentOwner));
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                owners.Add(new Owner
                {
                    Id = (int)row[nameof(Owner.Id)],
                    Name = row[nameof(Owner.Name)].ToString()
                });
            }
            return owners;
        }

        public static Owner GetOwner(int id)=> LoadApartmentOwner().FirstOrDefault(owner => owner.Id == id);
        public static Status GetStatus(int id)=> LoadApartmentStatus().FirstOrDefault(status => status.Id == id);
        public static City GetCity(int id)=> LoadCity().FirstOrDefault(city => city.Id == id);

        public static List<Status> LoadApartmentStatus()
        {
            List<Status> statuses = new List<Status>();
            ds = SqlHelper.ExecuteDataset(cs, nameof(LoadApartmentStatus));
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                statuses.Add(new Status
                {
                    Id = (int)row[nameof(Status.Id)],
                    Name = row[nameof(Status.Name)].ToString()
                });
            }
            return statuses;
        }
        public static List<City> LoadCity()
        {
            List<City> cities = new List<City>();
            ds = SqlHelper.ExecuteDataset(cs, nameof(LoadCity));
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                cities.Add(new City
                {
                    Id = (int)row[nameof(City.Id)],
                    Name = row[nameof(City.Name)].ToString()
                });
            }
            return cities;
        }

        public static User AuthUser(string username, string password)
        {
            ds = SqlHelper.ExecuteDataset(cs, nameof(AuthUser), username, password);
            DataRow row = ds.Tables[0].Rows[0];
            return new User
            {
                Id = row[nameof(User.Id)].ToString(),
                UserName = row[nameof(User.UserName)].ToString(),
                PasswordHash = row[nameof(User.PasswordHash)].ToString(),
                Address = row[nameof(User.Address)].ToString(),
                Email = row[nameof(User.Email)].ToString(),
                PhoneNumber = row[nameof(User.PhoneNumber)].ToString()
            };
        }

        public static void AddUser(string username, string password, string email, string phoneNumber, string address)
            => SqlHelper.ExecuteDataset(cs, nameof(AddUser), email, password, phoneNumber, username, address);
    }
}
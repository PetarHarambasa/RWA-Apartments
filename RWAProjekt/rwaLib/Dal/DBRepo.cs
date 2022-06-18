using Microsoft.ApplicationBlocks.Data;
using rwaLib.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rwaLib.Dal
{
    public class DBRepo : IRepo
    {
        private static string CS = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        public User AuthUser(string username, string password)
        {
            var tblAuth = SqlHelper.ExecuteDataset(CS, nameof(AuthUser), username, password).Tables[0];
            if (tblAuth.Rows.Count == 0) return null;

            DataRow row = tblAuth.Rows[0];
            return new User
            {
                Id = (int)row[nameof(User.Id)],
                Username = row[nameof(User.Username)].ToString(),
                PasswordHash = row[nameof(User.PasswordHash)].ToString(),
                Address = row[nameof(User.Address)].ToString(),
                Email = row[nameof(User.Email)].ToString(),
                PhoneNumber = row[nameof(User.PhoneNumber)].ToString(),
            };
        }

        public IList<User> LoadUsers()
        {
            IList<User> users = new List<User>();

            var tblUsers = SqlHelper.ExecuteDataset(CS, nameof(LoadUsers)).Tables[0];

            foreach (DataRow row in tblUsers.Rows)
            {
                users.Add(
                    new User
                    {
                        Id = (int)row[nameof(User.Id)],
                        Email = row[nameof(User.Email)].ToString(),
                        PasswordHash = row[nameof(User.PasswordHash)].ToString(),
                        PhoneNumber = row[nameof(User.PhoneNumber)].ToString(),
                        Username = row[nameof(User.Username)].ToString(),
                        Address = row[nameof(User.Address)].ToString(),
                    });
            }

            return users;
        }

        public void AddUser(User user)
        {
            SqlHelper.ExecuteNonQuery(CS, nameof(AddUser), user.Email, user.PasswordHash, user.PhoneNumber, user.Username, user.Address);
        }

        public void SaveUser(User user)
        {
            SqlHelper.ExecuteNonQuery(CS, nameof(SaveUser), user.Email, user.PhoneNumber, user.Username, user.Address, user.Id);
        }

        public void AddApartment(Apartment apartment)
        {
            SqlHelper.ExecuteDataset(CS, nameof(AddApartment), apartment.OwnerId, apartment.TypeId, apartment.StatusId, apartment.CityId, apartment.Address,
                apartment.Name, apartment.NameEng, apartment.Price, apartment.MaxAdults, apartment.MaxChildren, apartment.TotalRooms, apartment.BeachDistance);
        }

        public IList<Apartment> LoadApartments()
        {
            IList<Apartment> apartments = new List<Apartment>();

            var tblApartments = SqlHelper.ExecuteDataset(CS, nameof(LoadApartments)).Tables[0];

            foreach (DataRow row in tblApartments.Rows)
            {

              // if ((DateTime)row[nameof(Apartment.DeletedAt)] == null)
                //{

                    apartments.Add(
                        new Apartment
                        {
                            Id = (int)row[nameof(Apartment.Id)],
                            OwnerId = (int)row[nameof(Apartment.OwnerId)],
                            TypeId = (int)row[nameof(Apartment.TypeId)],
                            StatusId = (int)row[nameof(Apartment.StatusId)],
                            CityId = (int)row[nameof(Apartment.CityId)],
                            Address = row[nameof(Apartment.Address)].ToString(),
                            Name = row[nameof(Apartment.Name)].ToString(),
                            NameEng = row[nameof(Apartment.NameEng)].ToString(),
                            Price = (decimal)row[nameof(Apartment.Price)],
                            MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                            MaxChildren = (int)row[nameof(Apartment.MaxChildren)],
                            TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                            BeachDistance = (int)row[nameof(Apartment.BeachDistance)],
                        });

                //}
            }
            return apartments;
        }

        public IList<ApartmentOwner> LoadApartmentOwner()
        {
            IList<ApartmentOwner> apartmentOwner = new List<ApartmentOwner>();

            var tblApartmentOwners = SqlHelper.ExecuteDataset(CS, nameof(LoadApartmentOwner)).Tables[0];
            foreach (DataRow row in tblApartmentOwners.Rows)
            {
                apartmentOwner.Add(
                    new ApartmentOwner
                    {
                        Id = (int)row[nameof(ApartmentOwner.Id)],
                        Name = row[nameof(ApartmentOwner.Name)].ToString(),
                    }
                );
            }

            return apartmentOwner;
        }

        public IList<ApartmentStatus> LoadApartmentStatus()
        {
            IList<ApartmentStatus> apartmentStatuses = new List<ApartmentStatus>();

            var tblApartmentStatuses = SqlHelper.ExecuteDataset(CS, nameof(LoadApartmentStatus)).Tables[0];
            foreach (DataRow row in tblApartmentStatuses.Rows)
            {
                apartmentStatuses.Add(
                    new ApartmentStatus
                    {
                        Id = (int)row[nameof(ApartmentStatus.Id)],
                        Name = row[nameof(ApartmentStatus.Name)].ToString(),
                    }
                );
            }

            return apartmentStatuses;
        }

        public IList<City> LoadCity()
        {
            IList<City> cities = new List<City>();

            var tblCities = SqlHelper.ExecuteDataset(CS, nameof(LoadCity)).Tables[0];
            foreach (DataRow row in tblCities.Rows)
            {
                cities.Add(
                    new City
                    {
                        Id = (int)row[nameof(City.Id)],
                        Name = row[nameof(City.Name)].ToString(),
                    }
                );
            }

            return cities;
        }

        public void SaveApartment(Apartment apartment)
        {
            SqlHelper.ExecuteDataset(CS, nameof(SaveApartment), apartment.OwnerId, apartment.StatusId, apartment.CityId, apartment.Address,
                 apartment.Name, apartment.NameEng, apartment.Price, apartment.MaxAdults, apartment.MaxChildren, apartment.TotalRooms, apartment.BeachDistance, apartment.Id);
        }

        public void DeleteApartment(Apartment apartment)
        {
            SqlHelper.ExecuteDataset(CS, nameof(DeleteApartment), apartment.Id);
        }

        public IList<Tag> LoadTags()
        {
            IList<Tag> tags = new List<Tag>();
            var tblTags = SqlHelper.ExecuteDataset(CS, nameof(LoadTags)).Tables[0];
            foreach (DataRow row in tblTags.Rows)
            {
                tags.Add(
                    new Tag
                    {
                        Id = (int)row[nameof(Tag.Id)],
                        Name = row[nameof(Tag.Name)].ToString(),
                        NameEng = row[nameof(Tag.NameEng)].ToString(),
                        TypeId = (int)row[nameof(Tag.TypeId)],
                    });
            }

            return tags;
        }

        public IList<TagType> LoadTagTypes()
        {
            IList<TagType> tagTypes = new List<TagType>();
            var tblTagTypes = SqlHelper.ExecuteDataset(CS, nameof(LoadTagTypes)).Tables[0];
            foreach (DataRow row in tblTagTypes.Rows)
            {
                tagTypes.Add(
                    new TagType
                    {
                        Id = (int)row[nameof(Tag.Id)],
                        Name = row[nameof(Tag.Name)].ToString(),
                        NameEng = row[nameof(Tag.NameEng)].ToString()
                    });
            }

            return tagTypes;
        }

        public void SaveTag(Tag tag)
        {
            SqlHelper.ExecuteNonQuery(CS, nameof(SaveTag), tag.Name, tag.NameEng, tag.TypeId, tag.Id);
        }

        public void DeleteTag(Tag tag)
        {
            SqlHelper.ExecuteNonQuery(CS, nameof(DeleteTag), tag.Id);
        }

        public void AddTag(Tag tag)
        {
            SqlHelper.ExecuteNonQuery(CS, nameof(AddTag), tag.TypeId, tag.Name, tag.NameEng);
        }
    }
}

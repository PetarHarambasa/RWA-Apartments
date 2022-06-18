using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace MVC.Models.Auth
{
    public class DBContext : IDisposable
    {
        public static DataSet ds { get; set; }
        private static string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        public IList<User> Users{ get; set; }
        public DBContext(IList<User> users)
        {
            Users = users;
        }

        public void Dispose()
        {
            
        }

        public static List<User> LoadUsers()
        {
            List<User> users = new List<User>();
            ds = SqlHelper.ExecuteDataset(cs, nameof(LoadUsers));

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var user = new User();

                user.Id = row[nameof(User.Id)].ToString();
                user.UserName = row[nameof(User.UserName)].ToString();
                user.PasswordHash = row[nameof(User.PasswordHash)].ToString();
                user.Email = row[nameof(User.Email)].ToString();
                user.Address = row[nameof(User.Address)].ToString();
                user.PhoneNumber = row[nameof(User.Address)].ToString();
                users.Add(user);
            }

            return users;
        }

        public static DBContext Create()
        {
            return new DBContext(LoadUsers());
        }
    }
}
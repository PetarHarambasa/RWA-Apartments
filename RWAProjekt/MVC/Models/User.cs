using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace MVC.Models
{
    public class User : IUser
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ne smije biti prazan")]
        public string Email{ get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Lozinka ne smije biti prazna")]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "PhoneNumber ne smije biti prazan")]
        
        public string PhoneNumber { get; set; }

        [Display(Name = "username")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username ne smije biti prazan")]
        public string UserName { get; set; }
       
        [Required(AllowEmptyStrings = false, ErrorMessage = "Address ne smije biti prazna")]
        public string Address { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime UpdatedTime { get; set; }

        public User()
        {
            
        }
        public User(DataRow row)
        {
            Id = row["id"].ToString();
            Email = row["Email"].ToString();
            PhoneNumber = row["PhoneNumber"].ToString();
            UserName = row["UserName"].ToString();
            Address = row["Address"].ToString();
        }

        public User(string username, string password, string email, string phoneNumber, string address)
        {
            UserName = username;
            PasswordHash = password;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}
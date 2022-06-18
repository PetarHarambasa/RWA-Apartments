using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email ne smije biti prazan")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lozinka ne smije biti prazna")]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "PhoneNumber ne smije biti prazan")]
        public string PhoneNumber { get; set; }

        [Display(Name = "username")]
        [Required(ErrorMessage = "Username ne smije biti prazan")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Address ne smije biti prazna")]
        public string Address { get; set; }
    }
}
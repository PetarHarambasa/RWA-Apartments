using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Korisničko ime je obavezan")]
        [Display(Name ="Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lozinka je obavezan")]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string PasswordHash { get; set; }

        [Display(Name = "Zapamiti me")]
        public bool RememberMe { get; set; }
    }
}
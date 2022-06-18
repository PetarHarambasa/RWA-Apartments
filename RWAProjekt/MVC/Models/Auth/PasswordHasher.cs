using Microsoft.AspNet.Identity;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models.Auth
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return Processes.SHA512(password);
        }

        public PasswordVerificationResult VerifyHashedPassword
            (string hashedPassword, string providedPassword)
        {
            if (hashedPassword == HashPassword(providedPassword))
                return PasswordVerificationResult.Success;
            return PasswordVerificationResult.Failed;
        }
    }
}
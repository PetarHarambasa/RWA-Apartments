using Microsoft.AspNet.Identity;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MVC.Models.Auth
{
    public class UserStoreService<TUser> : IUserStore<TUser>,
    IUserPasswordStore<TUser>,
    IUserRoleStore<TUser>,
    IUserEmailStore<TUser>
    where TUser : User
    {
        private readonly IList<TUser> _users;


        public UserStoreService(IList<TUser> users)
        {
            _users = users;
        }

        public virtual Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public virtual Task<string> GetPasswordHashAsync(TUser user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public virtual Task<bool> HasPasswordAsync(TUser user)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public virtual void Dispose()
        {
        }

        public virtual Task CreateAsync(TUser user)
        {
            user.CreatedTime = DateTime.Now;
            user.UpdatedTime = DateTime.Now;
            _users.Add(user);
            return Task.FromResult(true);
        }

        public virtual Task UpdateAsync(TUser user)
        {
            user.UpdatedTime = DateTime.Now;
            _users.Remove(user);
            _users.Add(user);
            return Task.FromResult(true);
        }

        public virtual Task DeleteAsync(TUser user)
        {
            return Task.FromResult(_users.Remove(user));
        }


        public virtual Task<TUser> FindByIdAsync(string userId)
        {
            return Task.FromResult(_users.FirstOrDefault(u => u.Id == userId));
        }

        public virtual Task<TUser> FindAsync(string email, string password)
        {
            return Task.FromResult(_users.FirstOrDefault(u => u.UserName == email));
        }

        public virtual Task<TUser> FindByNameAsync(string username)
        {
            return Task.FromResult(_users.FirstOrDefault(u => u.UserName == username));
        }

        public virtual Task<TUser> FindByEmailAsync(string email)
        {
            return Task.FromResult(_users.FirstOrDefault(u => u.Email == email));
        }

        public Task AddToRoleAsync(TUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(TUser user)
        {
            IList<string> roles = new List<string>();
            return Task.FromResult(roles);
        }

        public Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(TUser user, string email)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmailAsync(TUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            throw new NotImplementedException();
        }
    }
}
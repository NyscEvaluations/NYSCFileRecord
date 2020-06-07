using NYSCFileRecord.Data;
using NYSCFileRecord.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NYSCFileRecord.Repositories.Services
{
    public class AdministratorRepository
    {
        private int UserId;
        public async Task<string> SaveUserInfo(ApplicationDbContext _db, User user)
        {
            var result = string.Empty;

            var userInfo = new User
            {
                UserId = user.UserId,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                StreetAddress = user.StreetAddress,
                City = user.City,
                State = user.State,
                IsActive = true,
                DateCreated = DateTime.UtcNow
            };

            try
            {
                _db.User.Add(userInfo);
                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {

                result = e.Message;
            }
            UserId = userInfo.UserId;
            return result;
        }

        public async Task<string> UpdateUserInfo(ApplicationDbContext _db, User user)
        {
            var result = string.Empty;


            try
            {
                var UserInfo = _db.User.SingleOrDefault(u => u.UserId == user.UserId);

                UserInfo.FirstName = user.FirstName;
                UserInfo.LastName = user.LastName;
                UserInfo.Email = user.Email;
                UserInfo.PhoneNumber = user.PhoneNumber;
                UserInfo.StreetAddress = user.StreetAddress;
                UserInfo.City = user.City;
                UserInfo.State = user.State;
                UserInfo.PostalCode = user.PostalCode;
                UserInfo.Picture = user.Picture;

                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {

                result = e.Message;
            }

            return result;
        }

        public async Task<string> DeleteUserInfo(ApplicationDbContext _db, User user)
        {
            var result = string.Empty;


            try
            {
                var UserInfo = _db.User.SingleOrDefault(u => u.UserId == user.UserId);

                UserInfo.IsActive = false;

                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {

                result = e.Message;
            }

            return result;
        }
    }
}

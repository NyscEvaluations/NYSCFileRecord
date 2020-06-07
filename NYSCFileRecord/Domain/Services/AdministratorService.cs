using NYSCFileRecord.Data;
using NYSCFileRecord.Models;
using NYSCFileRecord.Repositories.Queries;
using NYSCFileRecord.Repositories.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NYSCFileRecord.Domain.Services
{
    public class AdministratorService
    {
        AdministratorQueries administratorQueries = new AdministratorQueries();
        AdministratorRepository administratorRepository = new AdministratorRepository();

        public async Task<string> CreateNewUser(ApplicationDbContext _db, User user)
        {
            user.Password = "1234567";
            var processingMessage = string.Empty;
            var isDataOkay = true;

            var registrationInfo = await administratorQueries.GetUserByUsername(_db, user.Username);

            var isRecordExist = (registrationInfo == null) ? false : true;

            if (isRecordExist)
            {
                processingMessage = "User already Exist";
                isDataOkay = false;
            }

            if (isDataOkay)
            {
                registrationInfo = await administratorQueries.GetUserByEmail(_db, user.Email);
                isRecordExist = (registrationInfo == null) ? false : true;
                isDataOkay = isRecordExist ? false : true;
            }

            if (!isDataOkay)
            {
                processingMessage = "Email Used by Another User, please use another Email";
            }

            user.Password = Crypto.Encrypt(user.Password);

            var savedData = administratorRepository.SaveUserInfo(_db, user);

            return processingMessage;
        }

        public async Task<string> UpdateUser(ApplicationDbContext _db, User user)
        {
            var processingMessage = string.Empty;

            var updatedData = await administratorRepository.UpdateUserInfo(_db, user);

            return processingMessage;
        }

        public async Task<string> DeleteUser(ApplicationDbContext _db, User user)
        {
            var processingMessage = string.Empty;

            var updatedData = await administratorRepository.DeleteUserInfo(_db, user);

            return processingMessage;
        }


        public async Task<User> UserDetail(ApplicationDbContext _db, int? UserId)
        {
            var message = string.Empty;

            var userInfo = await administratorQueries.GetUser(_db, UserId);

            if (userInfo == null)
            {
                message = "No User exist in this Sytsem";
            }

            return userInfo;
        }

        public async Task<List<User>> UserList(ApplicationDbContext _db)
        {
            var message = string.Empty;

            var userInfo = await administratorQueries.GetAllUsers(_db);

            if (userInfo == null)
            {
                message = "No User exist in this Sytsem";
            }

            return userInfo;
        }
    }
}

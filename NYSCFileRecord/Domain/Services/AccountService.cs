//using Microsoft.AspNetCore.Http;
//using NYSCFileRecord.Data;
//using NYSCFileRecord.Models;
//using NYSCFileRecord.Repositories.Queries;
//using NYSCFileRecord.Repositories.Services;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace NYSCFileRecord.Domain.Services
//{
//    public class AccountService
//    {
//        //private ApplicationDbContext _db { get; set; }
//        public int UserId = 0;

//        //AccountQueries accountQueries = new AccountQueries();
//        //AccountRepository accountRepo = new AccountRepository();
//        public async Task<string> RegisterNewUser(ApplicationDbContext _db, User user)
//        {
//            var processingMessage = string.Empty;
//            var isDataOkay = true;

//            var registrationInfo = await accountQueries.GetUserByCodeNumber(_db, user.Username);

//            var isRecordExist = (registrationInfo == null) ? false : true;

//            if (isRecordExist)
//            {
//                processingMessage = "User already Exist";
//                isDataOkay = false;
//            }

//            if (isDataOkay)
//            {
//                //registrationInfo = await accountQueries.GetUserByEmail(_db, user.Email);
//                isRecordExist = (registrationInfo == null) ? false : true;
//                isDataOkay = isRecordExist ? false : true;
//            }

//            if (!isDataOkay)
//            {
//                processingMessage = "Email Used by Another User, please use another Email";
//            }

//            user.Password = Crypto.Encrypt(user.Password);

//            var savedData = accountRepo.SaveUserInfo(_db, user);

//            return processingMessage;
//        }

//        public async Task<bool> Login(ApplicationDbContext db, User user)
//        {
            

//            var message = string.Empty;

//            var userInfo = await accountQueries.GetUserByCodeNumber(db, user.Email);

//            if(userInfo == null)
//            {
//                userInfo = await accountQueries.GetUserByEmail(db, user.Email);
//            }

//            if(userInfo == null )
//            {
//                message = "You don't exist in our system or you have been blocked";
//            }

//            var encryptedPwd = Crypto.Encrypt(user.Password);

//            var decryptedPwd = Crypto.Decrypt(userInfo.Password);

//            if(!user.Password.Equals(decryptedPwd))
//            {
//                message = "Username or Password is not correct";
//                return false;
//            }

//            //UserId = userInfo.UserId;

//            return true;
//        }

//        public async Task<List<User>> GetAllUsers(ApplicationDbContext db)
//        {
//            var message = string.Empty;

//            var userInfo = await accountQueries.GetAllUsers(db);

//            if(userInfo == null)
//            {
//                message = "No User exist in this Sytsem";
//            }

//            return userInfo.ToList();
//        }

//        public async Task<User> GetUser(ApplicationDbContext db, int? UserId)
//        {
//            var message = string.Empty;

//            var userInfo = await accountQueries.GetUser(db, UserId);

//            if (userInfo == null)
//            {
//                message = "No User exist in this Sytsem";
//            }

//            return userInfo;
//        }

//        public async Task<List<User>> Gettop8Users(ApplicationDbContext db)
//        {
//            var message = string.Empty;

//            var userInfo = await accountQueries.GetTop8Users(db);

//            if (userInfo == null)
//            {
//                message = "No User exist in this Sytsem";
//            }

//            return userInfo.ToList();
//        }



//        public async Task<string> UpdateUser(ApplicationDbContext _db, User user)
//        {
//            var processingMessage = string.Empty;

//            var updatedData = await accountRepo.UpdateUserInfo(_db, user);

//            return processingMessage;
//        }
//    }
//}

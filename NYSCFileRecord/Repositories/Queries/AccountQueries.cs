﻿//using Microsoft.EntityFrameworkCore;
//using NYSCFileRecord.Data;
//using NYSCFileRecord.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace NYSCFileRecord.Repositories.Queries
//{
//    public class AccountQueries
//    {
//        public async Task<User> GetUserByCodeNumber(ApplicationDbContext db, string stateCode) 
//        {
//            var result = await (from u in db.User
//                                where u.Username.Equals(stateCode)
//                                select new User
//                                {
//                                    UserId = u.UserId,
//                                    Username = u.Username,
//                                    FirstName = u.FirstName,
//                                    LastName = u.LastName,
//                                    Email = u.Email,
//                                    Password = u.Password,
//                                    PhoneNumber = u.PhoneNumber,
//                                    StreetAddress = u.StreetAddress,
//                                    City = u.City,
//                                    State = u.State,
//                                    PostalCode = u.PostalCode,
//                                    Picture = u.Picture,
//                                    IsActive = u.IsActive,
//                                    DateCreated = u.DateCreated,
//                                    DateVerified = u.DateVerified
//                                }).FirstOrDefaultAsync();

//            return result;
//        }


//        public async Task<User> GetUserByEmail(ApplicationDbContext db, string email)
//        {
//            var result = await (from u in db.User
//                                where u.Email.Equals(email)
//                                select new User
//                                {
//                                    UserId = u.UserId,
//                                    Username = u.Username,
//                                    FirstName = u.FirstName,
//                                    LastName = u.LastName,
//                                    Email = u.Email,
//                                    Password = u.Password,
//                                    PhoneNumber = u.PhoneNumber,
//                                    StreetAddress = u.StreetAddress,
//                                    City = u.City,
//                                    State = u.State,
//                                    PostalCode = u.PostalCode,
//                                    Picture = u.Picture,
//                                    IsActive = u.IsActive,
//                                    DateCreated = u.DateCreated,
//                                    DateVerified = u.DateVerified
//                                }).FirstOrDefaultAsync();

//            return result;
//        }


//        public async Task<List<User>> GetAllUsers(ApplicationDbContext _db)
//        {
//            //IQueryable<object> result;

//            var result = await (from u in _db.User
//                                where u.IsActive.Equals(true)
//                                select new User
//                                {
//                                    UserId = u.UserId,
//                                    Username = u.Username,
//                                    FirstName = u.FirstName,
//                                    LastName = u.LastName,
//                                    Email = u.Email,
//                                    PhoneNumber = u.PhoneNumber,
//                                    StreetAddress = u.StreetAddress,
//                                    City = u.City,
//                                    State = u.State,
//                                    Picture = u.Picture,
//                                    IsActive = u.IsActive,
//                                    DateCreated = u.DateCreated, 
//                                    DateVerified = u.DateVerified
//                                }).ToListAsync();

//            return result;
//        }

//        public async Task<List<User>> GetTop8Users(ApplicationDbContext _db)
//        {
//            //IQueryable<object> result;

//            var result = await (from u in _db.User
//                                where u.IsActive.Equals(true)
//                                select new User
//                                {
//                                    UserId = u.UserId,
//                                    Username = u.Username,
//                                    FirstName = u.FirstName,
//                                    LastName = u.LastName,
//                                    Email = u.Email,
//                                    PhoneNumber = u.PhoneNumber,
//                                    StreetAddress = u.StreetAddress,
//                                    City = u.City,
//                                    State = u.State,
//                                    Picture = u.Picture,
//                                    IsActive = u.IsActive,
//                                    DateCreated = u.DateCreated,
//                                    DateVerified = u.DateVerified
//                                }).OrderByDescending(u => u.UserId).Take(8).ToListAsync();

//            return result;
//        }

//        public async Task<User> GetUser(ApplicationDbContext _db, int? UserId)
//        {
//            //IQueryable<object> result;

//            var result = await (from u in _db.User
//                                where u.UserId.Equals(UserId) && u.IsActive.Equals(true)
//                                select new User
//                                {
//                                    UserId = u.UserId,
//                                    Username = u.Username,
//                                    FirstName = u.FirstName,
//                                    LastName = u.LastName,
//                                    Email = u.Email,
//                                    PhoneNumber = u.PhoneNumber,
//                                    StreetAddress = u.StreetAddress,
//                                    City = u.City,
//                                    State = u.State,
//                                    Picture = u.Picture,
//                                    IsActive = u.IsActive,
//                                    DateCreated = u.DateCreated,
//                                    DateVerified = u.DateVerified
//                                }).FirstOrDefaultAsync();

//            return result;
//        }
//    }
//}

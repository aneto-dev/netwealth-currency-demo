﻿#pragma warning disable CA1416 // Validate platform compatibility
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Netwealth.Data;
using NetWealth.Data.Entities;

namespace QUBE.Web.API.Services
{
    public interface IUserService
    {
        public Task<ApiUser> GetValidUserByKey(string apiKey);
        public Task<ApiUser> GetValidUserById(long userId);
        Task<List<ApiUser> >GetAll();
       // TblUser GetById(int id);
    }

    public class UserService : IUserService
    {
        private NetwealthDbContext _context;

        public UserService(
            NetwealthDbContext context)
        {
            _context = context;
        }

        public async Task<ApiUser> GetValidUserByKey(string apiKey)
        {
            var user = await _context.ApiUsers.SingleOrDefaultAsync(x => x.Key == apiKey);

            return user;
        }

        public async Task<ApiUser> GetValidUserById(long userId)
        {
            var user = await _context.ApiUsers.SingleOrDefaultAsync(x => x.UserId == userId);

            return user;
        }

        public async Task<List<ApiUser> >GetAll()
        {
           return await _context.ApiUsers.ToListAsync();
        }

    }
}
#pragma warning restore CA1416 // Validate platform compatibility

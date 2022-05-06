#pragma warning disable CA1416 // Validate platform compatibility
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Netwealth.Data;
using NetWealth.Data.Entities;
using QUBE.Web.API.Services;

namespace NetWealth.Services.Implementation
{
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

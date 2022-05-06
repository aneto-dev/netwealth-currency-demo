using System.Collections.Generic;
using System.Threading.Tasks;
using NetWealth.Data.Entities;

namespace QUBE.Web.API.Services
{
    public interface IUserService
    {
        public Task<ApiUser> GetValidUserByKey(string apiKey);
        public Task<ApiUser> GetValidUserById(long userId);
        Task<List<ApiUser> >GetAll();
    }
}
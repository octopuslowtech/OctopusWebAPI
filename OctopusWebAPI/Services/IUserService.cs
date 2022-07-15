using OctopusWebAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OctopusWebAPI.Services
{
    public interface IUserService
    {
        public Task<UserInfo> CreateNew(UserInfo user);
        public Task<IEnumerable<UserInfo>> GetAllUser();

    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OctopusWebAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OctopusWebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly MyOctpDBContext _context;
        public UserService(MyOctpDBContext context)
        {
            _context = context;
        }
        public async Task<UserInfo> CreateNew(UserInfo user)
        {
            await _context.UserInfo.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<UserInfo>> GetAllUser()
        {
            return await _context.UserInfo.ToListAsync();
        }
    }
}

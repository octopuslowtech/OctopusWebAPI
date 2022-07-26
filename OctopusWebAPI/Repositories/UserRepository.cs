using Microsoft.EntityFrameworkCore;
using OctopusWebAPI.Data;
using OctopusWebAPI.Entities;

namespace OctopusWebAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User> Validate(User user);
        Task<User> CreateNewUser(User user);
        Task<User> Login(User user);
        Task<RefreshToken> AddRefreshToken(RefreshToken token);
    }
    public class UserRepository : IUserRepository
    {
        private readonly MyOctpDBContext _context;
        public UserRepository(MyOctpDBContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken> AddRefreshToken(RefreshToken token)
        {
            _context.RefreshTokens.Add(token);
            await _context.SaveChangesAsync();
            return token;
        }

        public async Task<User> CreateNewUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Login(User user)
        {
            return await _context.Users.Where(u => u.UserID == user.UserID
                                               && u.Password == user.Password).FirstOrDefaultAsync();
        }

        

        public async Task<User> Validate(User user)
        {
            return await _context.Users.FindAsync(user);
        }
    }
}

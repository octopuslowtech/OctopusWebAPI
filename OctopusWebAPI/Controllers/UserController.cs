using Microsoft.AspNetCore.Mvc;
using OctopusWebAPI.Data;

namespace OctopusWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly MyOctpDBContext _myOctpDBContext;
        public UserController(MyOctpDBContext myOctpDBContext)
        {
            _myOctpDBContext = myOctpDBContext;
        }
        [HttpPost]
        public async Task<IActionResult> CreateNew(UserInfo user)
        {
            if (user.UserName.Length < 1 && user.Password.Length < 1)
                return Ok(new
                {
                    Username = user.UserName,
                    Password = user.Password,
                    Message = "Error when create"
                });
            try
            {
                await _myOctpDBContext.AddAsync(user);
                await _myOctpDBContext.SaveChangesAsync();
                return Ok(new
                {
                    Username = user.UserName,
                    Password = user.Password,
                    Message = "Create Success"
                });
            }
            catch { }
            return BadRequest(new
            {
                Message = "Error when create"
            });
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserInfo user)
        {
            if (user.UserName.Length < 1 && user.Password.Length < 1)
                return Ok(new
                {
                    Message = "Fail"
                });
            try
            {
                var cakes = _myOctpDBContext.UserInfo.SingleOrDefault(p => p.);
                

                var _user = _myOctpDBContext.UserInfo.Where(p => p.UserName == user.UserName && p.Password == user.Password).ToList();
                 return Ok(new
                {
                    Username = user.UserName,
                    Password = user.Password,
                    Message = "Create Success"
                });
            }
            catch { }
            return BadRequest(new
            {
                Message = "Error when create"
            });
        }
    }
}

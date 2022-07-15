using Microsoft.AspNetCore.Mvc;
using OctopusWebAPI.Data;
using OctopusWebAPI.Services;

namespace OctopusWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }
        [HttpPost("CreateNew")]
        public async Task<IActionResult> CreateNew([FromBody] UserInfo user)
        {
            if (user.UserName.Length < 1 || user.Password.Length < 1)
                return Ok(new
                {
                    Username = user.UserName,
                    Password = user.Password,
                    Message = "Error when create"
                });
            try
            {
                var _user = await _service.CreateNew(user);
                return Ok(new
                {
                    Username = _user.UserName,
                    Password = _user.Password,
                    Message = "Create Success"
                });
            }
            catch { }
            return BadRequest(new
            {
                Message = "Error when create"
            });
        }


        [HttpPost("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var _user = await _service.GetAllUser();
                return Ok(new
                {
                    _user,
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

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
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

        [HttpPost("UploadBackup")]
        public async Task<IActionResult> UploadBackup(IFormFile file)
        {
             try
             {
                if(await Extention.WriteFile(file))
                {
                    return Ok(new
                    {
                        id = file.FileName.Replace(" ",""),
                        Message = "Upload Success"
                    });
                }
            }
            catch { }
            return BadRequest(new
            {
                Message = "Error when Upload"
            });
        }

        [HttpGet("DownloadBackup/{id}")]
        public async Task<IActionResult> DownloadBackup(string id)
        {
            try
            {
                var path2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\backup\\" + id);
                if (!System.IO.File.Exists(path2))
                    return BadRequest(new
                    {
                        id = id,
                        Message = "Error when Download"
                    });

                var fileName = System.IO.Path.GetFileName(path2);
                var content = await System.IO.File.ReadAllBytesAsync(path2);
                new FileExtensionContentTypeProvider()
                    .TryGetContentType(fileName, out string contentType);
                return File(content, contentType, fileName);
            }
            catch { }
            return BadRequest(new
            {
                id = id,
                Message = "Error when Download"
            });
        }
    }
}

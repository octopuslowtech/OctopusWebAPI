using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.IdentityModel.Tokens;
using OctopusModel;
using OctopusWebAPI.Data;
using OctopusWebAPI.Entities;
using OctopusWebAPI.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OctopusWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _service;
        private readonly IConfiguration _configuration;
        public UserController(IUserRepository service, IConfiguration configuration) 
        {
            _service = service;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] UserModel usermodel)
        {
            var user = await _service.Login(new Entities.User
            {
                UserID = usermodel.UserID,
                Password = usermodel.Password,
            });
            if(user == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            try
            {
                RefreshToken refreshToken = GenerateRefreshToken();
                refreshToken.TokenId = Guid.NewGuid();
                refreshToken.UserID = user.UserID;
                await _service.AddRefreshToken(refreshToken);
                var AccessToken = GenerateAccessToken(user);
                return Ok(new { message = "success", id = user.UserID, accesstoken = AccessToken, refreshtoken = refreshToken.Token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromBody] UserModel user)
        {
            if (user == null)
                return BadRequest(new { message = "Error value" });
            try
            {
                var user2 = await _service.CreateNewUser(new Entities.User
                {
                    UserID = user.UserID,
                    Password = user.Password,
                    DateCreate = DateTime.Now,
                });
                 if (user2 == null)
                    return BadRequest(new { message = "Error when create" });
                 return Ok(new {message = "success", id = user2.UserID, password = user2.Password});
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            return NotFound();

        }

        [HttpPost("UploadBackup")]
        [Authorize]
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
        [Authorize]
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
        private RefreshToken GenerateRefreshToken()
        {
            RefreshToken refreshToken = new RefreshToken();
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken.Token = Convert.ToBase64String(randomNumber);
            }
            refreshToken.ExpiryDate = DateTime.UtcNow.AddDays(1);

            return refreshToken;
        }

        private string GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSecurityKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Phone", user.UserID),
                    new Claim("DateCreate", user.DateCreate.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

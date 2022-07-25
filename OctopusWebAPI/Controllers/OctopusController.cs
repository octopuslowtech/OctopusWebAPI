using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OctopusWebAPI.Data;
using System.Diagnostics;

namespace OctopusWebAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class OctopusController : Controller
    {
        [HttpPost("Captcha")]
        [Authorize]
        public async Task<IActionResult> Captcha([FromBody] string base64string)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();
            if (base64string.Length < 1)
                return Ok(new
                {
                    Message = "fail"
                });
            var Point = await Extention.CaptchaDemo(base64string);
            if(Point.IsEmpty)
            {
                return Ok(new
                {
                    Message = "fail"
                });
            }
            stopWatch.Stop();
            TimeSpan timespan = stopWatch.Elapsed;
            return Ok(new
            {
                Time = timespan.TotalMilliseconds.ToString("0.0###"),
                X = Point.X,
                Y = Point.Y,
                Message = "Success"
            });
        }
 
        [HttpGet("Get2FA")]
        [Authorize]
        public async Task<IActionResult> Get2FA(string twofa)
        {
            if(twofa.Length < 1)
                return Ok(new
                {
                    Message = "fail"
                });
            string otp = await Extention.GetTWOFACode(twofa);

            return Ok(new
            {
                code = otp,
                Message = "Success"
            });
        }
    }
}

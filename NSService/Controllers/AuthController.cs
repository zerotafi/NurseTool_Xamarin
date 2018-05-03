using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NSService.Entities;
using NSService.Services;

namespace NSService.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private IPatientInfoRepository _patientInfoRepository;
        private ILogger<ExaminationController> _logger;

        public AuthController(ILogger<ExaminationController> logger, IPatientInfoRepository patientInfoRepository)
        {
            _patientInfoRepository = patientInfoRepository;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Auth([FromBody] User body)
        {
            if (body == null || body.Username == null || body.Password == null ||
              body.Username.Length == 0 || body.Password.Length == 0)
            {
                return BadRequest(); ;
            }

            if (!IsValidUser(body))
            {
                return Unauthorized();
            }
            var claims = new Claim[]
           {
                new Claim(JwtRegisteredClaimNames.Sub, body.Username)
            };

            return Ok(new LoginResult()
            {
                AuthenticationToken = new Guid().ToString(),
                User = new LoginResultUser { UserId = body.Username }
            });
        }

        private bool IsValidUser(User user)
        {
            return _patientInfoRepository.Auth(user.Username, user.Password);
        }
    }

    public class LoginResult
    {
        [JsonProperty(PropertyName = "authenticationToken")]
        public string AuthenticationToken { get; set; }

        [JsonProperty(PropertyName = "user")]
        public LoginResultUser User { get; set; }
    }
    public class LoginResultUser
    {
        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }
    }
}
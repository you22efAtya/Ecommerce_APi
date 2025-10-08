using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class AuthenticationController(IServiceManager _serviceManager) : ApiController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDto>> Login(LoginDto loginDto)
        {
            var result = await _serviceManager.AuthenticationService.Login(loginDto);
            return Ok(result);
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserResultDto>> Register(RegisterDto registerDto)
        {
            var result = await _serviceManager.AuthenticationService.Register(registerDto);
            return Ok(result);
        }
    }
}

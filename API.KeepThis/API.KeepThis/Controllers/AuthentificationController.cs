﻿using API.KeepThis.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.KeepThis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthentificationController : ControllerBase
    {

        private readonly IAuthentificationService _AuthentificationService;
        public AuthentificationController(IAuthentificationService UsersService)
        {
            _AuthentificationService = UsersService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var user = await _AuthentificationService.AuthenticateUserAsync(request);
                //var token = _AuthentificationService.GenerateJwtToken(user);

                //return Ok(new { Token = token });

                return Ok(user);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Return 401 Unauthorized status code with error message
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle other potential exceptions
                // You might want to log the exception here
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }
    }
}

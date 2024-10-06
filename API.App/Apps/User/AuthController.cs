﻿using API.Service.Dtos.Auth;
using API.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.App.Apps.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var res = await _authService.Login(dto);
            return StatusCode(res.StatusCode, res.Data);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDto dto)
        {
            var res = await _authService.Register(dto);
            return StatusCode(res.StatusCode, res.Data);
        }
    }
}

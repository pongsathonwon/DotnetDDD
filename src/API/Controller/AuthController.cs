using API.Auth;
using Application.Auth.Login;
using Application.Auth.RegisterByAdmin;
using Application.Auth.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;

[ApiController]
[Route("api/auth")]
public class AuthController(TokenService tokenService, ISender sender) : ControllerBase
{
    private readonly TokenService _tokenService = tokenService;
    private readonly ISender _sender = sender;

    [HttpPost("token")]
    public async Task<IActionResult> GenerateToken(LoginQuery request)
    {

        var auth = await _sender.Send(request);

        var token = _tokenService.GenerateToken(
            userId: auth.UserId.ToString(),
            userName: auth.Username,
            role: auth.Role.ToString());

        return Ok(new { Message = "logged in successfully.", Token = token });

    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand request)
    {
        var auth = await _sender.Send(request);
        var token = _tokenService.GenerateToken(
            userId: auth.UserId.ToString(),
            userName: auth.Username,
            role: auth.Role.ToString());
        return Created("", new { Message = "User registered successfully.", Token = token });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("register/{id:Guid}")]
    public async Task<IActionResult> RegisterByAdmin(Guid id, LoginRequest request)
    {
        var auth = await _sender.Send(new RegisterByAdminCommand(
            CustomerId: id,
            Username: request.Username,
            Password: request.Password
        ));
        var token = _tokenService.GenerateToken(
            userId: auth.UserId.ToString(),
            userName: auth.Username,
            role: auth.Role.ToString());
        return Created("", new { Message = "User registered successfully.", Token = token });
    }
}

public sealed record LoginRequest(string Username, string Password);

using API.Auth;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;

[ApiController]
[Route("api/auth")]
public class AuthController(TokenService tokenService) : ControllerBase
{
    private readonly TokenService _tokenService = tokenService;

    [HttpPost("token")]
    public IActionResult GenerateToken(LoginRequest request)
    {
        if (request.Username == "admin" && request.Password == "password")
        {
            var token = _tokenService.GenerateToken(
                userId: Guid.NewGuid().ToString(),
                userName: request.Username,
                role: "Admin");

            return Ok(new { Token = token });
        }

        if (request.Username == "user" && request.Password == "password")
        {
            var token = _tokenService.GenerateToken(
                userId: Guid.NewGuid().ToString(),
                userName: request.Username,
                role: "User");

            return Ok(new { Token = token });
        }

        return Unauthorized(new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = "Authentication Failed",
            Detail = "Invalid username or password."
        });
    }
}

public sealed record LoginRequest(string Username, string Password);

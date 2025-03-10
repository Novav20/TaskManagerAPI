using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Services;

namespace TaskManager.Endpoints.Authentication;
public class LoginRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
}

public class LoginEndpoint(AppDbContext db, JwtServices jwtServices) : Endpoint<LoginRequest, LoginResponse>
{
    private readonly AppDbContext _db = db;
    private readonly JwtServices _jwtServices = jwtServices;
    public override void Configure()
    {
        Post("/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == req.Username, ct);
        if (user == null || !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
        {
            await SendAsync(new LoginResponse { Token = string.Empty }, 401, cancellation: ct);
            return;
        }
        var token = _jwtServices.GenerateJwtToken(user);
        await SendAsync(new LoginResponse { Token = token }, cancellation: ct);
    }
}

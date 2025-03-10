using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Endpoints.Authentication;
public class RegisterUserRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class RegisterUserEndpoint(AppDbContext db) : Endpoint<RegisterUserRequest>
{
    private readonly AppDbContext _db = db;
    public override void Configure()
    {
        Post("/register");
        AllowAnonymous();
    }
    public override async Task HandleAsync(RegisterUserRequest req, CancellationToken ct)
    {
        if (await _db.Users.AnyAsync(u => u.Username == req.Username, ct))
        {
            await SendAsync("User already exists", 400, cancellation: ct);
            return;
        }
        var user = new User
        {
            Username = req.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.Password)
        };
        _db.Users.Add(user);
        await _db.SaveChangesAsync(ct);

        await SendAsync("User registered successfully!", cancellation: ct);
    }
}

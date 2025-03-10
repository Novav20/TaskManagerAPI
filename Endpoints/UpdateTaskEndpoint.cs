using FastEndpoints;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Endpoints;

public class UpdateTaskEndpoint(AppDbContext db) : Endpoint<TaskItem, TaskItem>
{
    private readonly AppDbContext _db = db;
    public override void Configure()
    {
        Put("/tasks/{Id}");
        AuthSchemes("Bearer");
    }

    public override async Task HandleAsync(TaskItem req, CancellationToken ct)
    {
        var existing = _db.Tasks.FirstOrDefault(t => t.Id == req.Id);
        if (existing == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        _db.Entry(existing).CurrentValues.SetValues(req);
        await _db.SaveChangesAsync(ct);
        await SendAsync(req, cancellation: ct);
    }
}
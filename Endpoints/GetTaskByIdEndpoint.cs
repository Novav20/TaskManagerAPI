using FastEndpoints;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Endpoints;

public class GetTaskByIdRequest // DTO for the request
{
    public Guid Id { get; set; }
}

public class GetTaskByIdEndpoint(AppDbContext db) : Endpoint<GetTaskByIdRequest, TaskItem>
{
    private readonly AppDbContext _db = db;

    public override void Configure()
    {
        Get("/tasks/{Id}");
        AuthSchemes("Bearer");
    }

    public override async Task HandleAsync(GetTaskByIdRequest req, CancellationToken ct)
    {
        var task = _db.Tasks.FirstOrDefault(t => t.Id == req.Id);
        if (task == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        await SendAsync(task, cancellation: ct);
    }
}
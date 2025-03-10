using FastEndpoints;
using TaskManager.Data;

namespace TaskManager.Endpoints;
public class DeleteTaskRequest
{
    public Guid Id { get; set; }
}
public class DeleteTaskEndpoint(AppDbContext db) : Endpoint<DeleteTaskRequest, string>
{
    private readonly AppDbContext _db = db;
    public override void Configure()
    {
        Delete("/tasks/{Id}");
        AuthSchemes("Bearer");
    }

    public override async Task HandleAsync(DeleteTaskRequest req, CancellationToken ct)
    {
        var task = _db.Tasks.FirstOrDefault(t => t.Id == req.Id);
        if (task == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        _db.Tasks.Remove(task);
        await _db.SaveChangesAsync(ct);
        await SendOkAsync("Deleted", ct);
    }
}
using FastEndpoints;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Endpoints;
public class AddTaskRequest
    {
        // Only request fields for input; id and isComplete have default values in TaskItem
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

public class AddTaskEndpoint(AppDbContext db) : Endpoint<AddTaskRequest, TaskItem>
{
    private readonly AppDbContext _db = db;
    public override void Configure()
    {
        Post("/task");
        AuthSchemes("Bearer");
    }

    public override async Task HandleAsync(AddTaskRequest request, CancellationToken ct)
    {
        // Map AddTaskRequest to TaskItem (id and isComplete keep their default values)
        var task = new TaskItem
        {
            Title = request.Title,
            Description = request.Description
        };
        _db.Tasks.Add(task);
        await _db.SaveChangesAsync(ct);
        await SendAsync(task, cancellation: ct);
    }
}

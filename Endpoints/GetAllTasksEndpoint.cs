using FastEndpoints;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Endpoints;

public class GetAllTasksEndpoint(AppDbContext db) : EndpointWithoutRequest<TaskItem[]>
{
    private readonly AppDbContext _db = db;
    public override void Configure()
    {
        Get("/tasks");
        AuthSchemes("Bearer");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendAsync([.. _db.Tasks], cancellation: ct);
    }
}

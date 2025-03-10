using FastEndpoints;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Endpoints
{
    public class AddTasksEndpoint(AppDbContext db) : Endpoint<List<AddTaskRequest>, List<TaskItem>>
    {
        private readonly AppDbContext _db = db;
        public override void Configure()
        {
            Post("/tasks");
            AuthSchemes("Bearer");
        }

        public override async Task HandleAsync(List<AddTaskRequest> requests, CancellationToken ct)
        {
            var tasks = requests.Select(req => new TaskItem
            {
                Title = req.Title,
                Description = req.Description
            }).ToList();

            _db.Tasks.AddRange(tasks);
            await _db.SaveChangesAsync(ct);
            await SendAsync(tasks, cancellation: ct);
        }
    }
}
using InterviewDemo.Models;
using InterviewDemo.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using InterviewDemo;

var builder = Host.CreateApplicationBuilder(args);

var dbPath = Path.Combine(Environment.CurrentDirectory, "data.db");
builder.Services.AddDbContextFactory<DataContext>(opts => opts.UseSqlite("Data Source=" + dbPath));

builder.Services.AddSingleton<CRUDService>();
builder.Services.AddSingleton<ClientService>();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<DataContext>();
    await ctx.Database.MigrateAsync();

    ctx.Statuses.RemoveRange(ctx.Statuses);
    ctx.Schedules.RemoveRange(ctx.Schedules);
    ctx.Jobs.RemoveRange(ctx.Jobs);
    await ctx.SaveChangesAsync();

    if (!ctx.Jobs.Any())
    {
        var jsonPath = Path.Combine(Environment.CurrentDirectory, "MockData.json");
        var json = File.ReadAllText(jsonPath);
        var list = JsonSerializer.Deserialize<List<MockJobDto>>(json)!;

        foreach (var dto in list)
        {
            var job = new Job
            {
                Invoice = dto.Invoice,
                Name = dto.Name
            };
            ctx.Jobs.Add(job);
            ctx.SaveChanges();

            ctx.Schedules.Add(new Schedule
            {
                JobId = job.Id,
                StartDate = dto.StartDate,
                DueDate = dto.DueDate
            });
            ctx.Statuses.Add(new Status
            {
                JobId = job.Id,
                CurrentStatus = dto.Status,
                Location = dto.Location,
                LastInspector = dto.LastInspector
            });
            ctx.SaveChanges();
        }
    }
}

var client = app.Services.GetRequiredService<ClientService>();
await client.RunAsync();
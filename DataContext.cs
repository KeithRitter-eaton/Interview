using InterviewDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace InterviewDemo;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Job> Jobs { get; set; } = null!;
    public DbSet<Schedule> Schedules { get; set; } = null!;
    public DbSet<Status> Statuses { get; set; } = null!;
}
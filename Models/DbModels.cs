namespace InterviewDemo.Models;

public record Job
{
    public int Id { get; set; }
    public Guid Invoice { get; set; }
    public string Name { get; set; } = null!;
    public Schedule Schedule { get; set; } = null!;
    public Status Status { get; set; } = null!;
}

public record Schedule
{
    public int Id { get; set; }
    public int JobId { get; set; }
    public Job Job { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime DueDate { get; set; }
}

public record Status
{
    public int Id { get; set; }
    public int JobId { get; set; }
    public Job Job { get; set; } = null!;
    public string CurrentStatus { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string LastInspector { get; set; } = null!;
}
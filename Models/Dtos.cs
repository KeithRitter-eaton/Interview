namespace InterviewDemo.Models;

public record MockJobDto
{
    public int Id { get; set; }
    public Guid Invoice { get; set; }
    public string Name { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime DueDate { get; set; }
    public string Status { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string LastInspector { get; set; } = null!;
}
using InterviewDemo.Models;
using InterviewDemo.Enums;
using Microsoft.EntityFrameworkCore;

namespace InterviewDemo.Services;

public class CRUDService
{
    private readonly IDbContextFactory<DataContext> _factory;

    public CRUDService(IDbContextFactory<DataContext> factory)
        => _factory = factory;

    /// <summary>
    /// Your goal here is to create some new job data
    /// The ClientService that is done for you will prompt you for the required information to make the job.
    /// For easy checking, make the start date today and the due date tomorrow please.
    /// You can name it whatever you want. JobStatus and JobLocation should be the pending options.
    /// Make sure to create the entries for the job, schedule, and status tables.
    /// When done, make sure you print the job's invoice so you can copy and track it later.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="start"></param>
    /// <param name="due"></param>
    /// <param name="status">The selected job status</param>
    /// <param name="location">The selected job location</param>
    /// <param name="inspector">The inspector assigned to the job, default is "Unassigned"</param>
    public async Task CreateJobData(string name, DateTime start, DateTime due, JobStatus status, JobLocation location, string inspector = "Unassigned")
    {
        await using var ctx = await _factory.CreateDbContextAsync();

        // Create the job

        // Create the schedule

        // Create the status

        await ctx.SaveChangesAsync();
        Console.WriteLine($"Created job: ");
    }

    /// <summary>
    /// Goal is to remove data from the database based on the provided GUID
    /// </summary>
    /// <param name="guid"></param>
    public async Task RemoveJobData(Guid guid)
    {
        Console.WriteLine($"Removed job: {guid}");
    }

    /// <summary>
    /// Goal is to update the job data based on the provided GUID, you will need to update data in the respective tables
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="newName"></param>
    /// <param name="start"></param>
    /// <param name="due"></param>
    /// <param name="status"></param>
    /// <param name="location"></param>
    /// <param name="inspector"></param>
    public async Task UpdateJobData(
        Guid guid,
        string newName,
        DateTime start,
        DateTime due,
        JobStatus status,
        JobLocation location,
        string inspector)
    {
        Console.WriteLine($"Updated job: {guid}");
    }

    /// <summary>
    /// Your goal here is to fix the SQL query to fetch the job based on the provided GUID
    /// </summary>
    /// <param name="guid"></param>
    public async Task FetchJobData(Guid guid)
    {
        await using var ctx = await _factory.CreateDbContextAsync();
        
        // Use raw SQL query to fetch job data
        const string sql = """

                                       SELECT j.Id, j.Invoice, j.Name, 
                                              s.StartDate, s.DueDate,
                                              st.CurrentState, st.Location, st.LastInspector
                                       FROM Jobs j
                                       JOIN Schedules s ON j.Id = s.JobId
                                       JOIN Statuses st ON j.Id = st.JobId
                                       WHERE j.Name = @invoice
                           """;
            
        // Create a SQL parameter for the GUID
        var parameter = new Microsoft.Data.Sqlite.SqliteParameter("@invoice", guid);
        
        // Execute the query and get the results
        await using var command = ctx.Database.GetDbConnection().CreateCommand();
        command.CommandText = sql;
        command.Parameters.Add(parameter);
        
        if (command.Connection is not { State: System.Data.ConnectionState.Open })
            await command.Connection.OpenAsync();

        await using var result = await command.ExecuteReaderAsync();
        
        if (await result.ReadAsync())
        {
            Console.WriteLine("Job Information:");
            Console.WriteLine($"Invoice: {result.GetGuid(result.GetOrdinal("Invoice"))}");
            Console.WriteLine($"Name: {result.GetString(result.GetOrdinal("Name"))}");
            Console.WriteLine($"Start Date: {result.GetDateTime(result.GetOrdinal("StartDate"))}");
            Console.WriteLine($"Due Date: {result.GetDateTime(result.GetOrdinal("DueDate"))}");
            Console.WriteLine($"Status: {result.GetString(result.GetOrdinal("CurrentStatus"))}");
            Console.WriteLine($"Location: {result.GetString(result.GetOrdinal("Location"))}");
            Console.WriteLine($"Inspector: {result.GetString(result.GetOrdinal("LastInspector"))}");
        }
        else
        {
            Console.WriteLine($"No job found with invoice: {guid}");
        }
    }
}
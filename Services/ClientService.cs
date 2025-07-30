namespace InterviewDemo.Services;

using InterviewDemo.Enums;


/// <summary>
/// You shouldn't have to change anything here, this has been done for you.
/// It may not be the world's best code, but it does not affect the interview process.
/// </summary>
public class ClientService
{
    private readonly CRUDService _crud;
    public ClientService(CRUDService crud) => _crud = crud;

    public async Task RunAsync()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Choose an action:");
            Console.WriteLine(" 1. Create job");
            Console.WriteLine(" 2. Remove job");
            Console.WriteLine(" 3. Update job");
            Console.WriteLine(" 4. Fetch job");
            Console.WriteLine(" 5. Exit");
            Console.Write("→ ");

            if (!int.TryParse(Console.ReadLine(), out var choice))
                continue;

            switch (choice)
            {
                case 1:
                    Console.Write(" Name: ");
                    var name   = Console.ReadLine()!;
                    Console.Write(" Start (yyyy-MM-dd): ");
                    var sd     = DateTime.Parse(Console.ReadLine()!);
                    Console.Write(" Due   (yyyy-MM-dd): ");
                    var dd     = DateTime.Parse(Console.ReadLine()!);
                    
                    Console.WriteLine(" Select current status:");
                    var statusValues = Enum.GetValues<JobStatus>();
                    for (var i = 0; i < statusValues.Length; i++)
                    {
                        Console.WriteLine($"  {i + 1}. {statusValues[i]}");
                    }
                    Console.Write(" → ");
                    
                    var statusChoice = 1;
                    if (int.TryParse(Console.ReadLine(), out var choice1) && choice1 >= 1 && choice1 <= statusValues.Length)
                    {
                        statusChoice = choice1;
                    }
                    var selectedStatus = statusValues[statusChoice - 1];
                    
                    Console.WriteLine(" Select location:");
                    var locationValues = Enum.GetValues<JobLocation>();
                    for (var i = 0; i < locationValues.Length; i++)
                    {
                        Console.WriteLine($"  {i + 1}. {locationValues[i]}");
                    }
                    Console.Write(" → ");
                    
                    var locationChoice = 1;
                    if (int.TryParse(Console.ReadLine(), out var locChoice) && locChoice >= 1 && locChoice <= locationValues.Length)
                    {
                        locationChoice = locChoice;
                    }
                    var selectedLocation = locationValues[locationChoice - 1];
                    
                    await _crud.CreateJobData(name, sd, dd, selectedStatus, selectedLocation);
                    break;

                case 2:
                    Console.Write(" Invoice GUID: ");
                    var remGuid = Guid.Parse(Console.ReadLine()!);
                    await _crud.RemoveJobData(remGuid);
                    break;

                case 3:
                    Console.Write(" Invoice GUID: ");
                    var updGuid = Guid.Parse(Console.ReadLine()!);
                    Console.Write(" New name: ");
                    var newName = Console.ReadLine()!;
                    Console.Write(" New start (yyyy-MM-dd): ");
                    var newSd   = DateTime.Parse(Console.ReadLine()!);
                    Console.Write(" New due   (yyyy-MM-dd): ");
                    var newDd   = DateTime.Parse(Console.ReadLine()!);
                    
                    Console.WriteLine(" Select current status:");
                    var updateStatusValues = Enum.GetValues<JobStatus>();
                    for (var i = 0; i < updateStatusValues.Length; i++)
                    {
                        Console.WriteLine($"  {i + 1}. {updateStatusValues[i]}");
                    }
                    Console.Write(" → ");
                    
                    var updateStatusChoice = 1;
                    if (int.TryParse(Console.ReadLine(), out var updateChoice) && updateChoice >= 1 && updateChoice <= updateStatusValues.Length)
                    {
                        updateStatusChoice = updateChoice;
                    }
                    var updatedStatus = updateStatusValues[updateStatusChoice - 1];
                    
                    Console.WriteLine(" Select location:");
                    var updateLocationValues = Enum.GetValues<JobLocation>();
                    for (var i = 0; i < updateLocationValues.Length; i++)
                    {
                        Console.WriteLine($"  {i + 1}. {updateLocationValues[i]}");
                    }
                    Console.Write(" → ");
                    
                    var updateLocationChoice = 1;
                    if (int.TryParse(Console.ReadLine(), out var updateLocChoice) && updateLocChoice >= 1 && updateLocChoice <= updateLocationValues.Length)
                    {
                        updateLocationChoice = updateLocChoice;
                    }
                    var updatedLocation = updateLocationValues[updateLocationChoice - 1];
                    
                    Console.Write(" Inspector: ");
                    var insp = Console.ReadLine()!;
                    
                    await _crud.UpdateJobData(updGuid, newName, newSd, newDd, updatedStatus, updatedLocation, insp);
                    break;

                case 4:
                    Console.Write(" Invoice GUID: ");
                    var fGuid   = Guid.Parse(Console.ReadLine()!);
                    await _crud.FetchJobData(fGuid);
                    break;

                case 5:
                    return;
            }
        }
    }
}

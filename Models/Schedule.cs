public class Schedule
{
    private DateTime Date { get; set; }
    private string? Place { get; set; }
    private string? Task { get; set; }
    private Priority TaskPriority { get; set; }
    private User? TaskUser { get; set; }

    public Schedule(DateTime date, string place, string task, Priority taskPriority, User taskUser)
    {
        Date = date;
        Place = place;
        Task = task;
        TaskPriority = taskPriority;
        TaskUser = taskUser
    }
}

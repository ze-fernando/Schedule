public class Schedule
{
    private int Id { get; set; }
    private DateTime Date { get; set; }
    private string? Place { get; set; }
    private string? Task { get; set; }
    private Priority TaskPriority { get; set; }
    private User? TaskUser { get; set; }

}

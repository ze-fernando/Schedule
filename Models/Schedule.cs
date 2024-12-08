public class Schedule
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string? Place { get; set; }
    public string? Task { get; set; }
    public Priority TaskPriority { get; set; }
    public User? TaskUser { get; set; }

}

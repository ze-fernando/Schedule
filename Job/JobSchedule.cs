
using Quartz;
using Schedule.Services;

namespace Schedule.Job;

public class JobSchedule(EmailService service): IJob
{
    private readonly EmailService _service = service;

    public Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("alo");
        _service.SendSchedule();
        return Task.CompletedTask;
    }
}

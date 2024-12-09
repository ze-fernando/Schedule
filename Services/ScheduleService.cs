public static class ScheduleService
{
    public static Schedule CreateSchedule(ScheduleDto schedule)
    {
        var newSchedule = new Schedule{
            Date = schedule.Date,
            Place = schedule.Place,
            Task = schedule.Task,
            TaskPriority = schedule.TaskPriority,
            TaskUser = schedule.TaskUser
        };

        using(var context = new AppDbContext()){
            context.Schedules?.Add(newSchedule);
            context.SaveChanges();
        }

        return newSchedule;

    }
}

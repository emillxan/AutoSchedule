namespace AutoSchedule.BLL.Logic;

public static class ScheduleConstants
{
    public static readonly TimeSpan StartOfDay = new TimeSpan(9, 0, 0); // 9:00 утра
    public static readonly TimeSpan DurationOfLesson = new TimeSpan(0, 90, 0); // 90 минут
    public static readonly TimeSpan BreakTime = new TimeSpan(0, 10, 0); // 10 минут
    public static readonly int MaxLessonsPerDay = 4; // Максимум 4 пары в день
}

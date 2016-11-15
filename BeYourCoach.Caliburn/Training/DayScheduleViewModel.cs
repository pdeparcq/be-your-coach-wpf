using BeYourCoach.Domain.Training;
using NodaTime;

namespace BeYourCoach.Caliburn.Training
{
    public class DayScheduleViewModel
    {
        public Schedule Schedule { get; private set; }
        public int Week { get; private set; }
        public IsoDayOfWeek DayOfWeek { get; private set; }

        public DayScheduleViewModel(Schedule schedule, int week, IsoDayOfWeek dayOfWeek)
        {
            Schedule = schedule;
            Week = week;
            DayOfWeek = dayOfWeek;
        }
    }
}

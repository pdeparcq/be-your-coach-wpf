using Conditions.Guards;
using NodaTime;

namespace BeYourCoach.Domain.Training
{
    public class WeekSchedule
    {
        public Schedule Schedule { get; internal set; }
        public int Week { get; private set; }
        public int WeekOfWeekYear => StartDate.WeekOfWeekYear;
        public LocalDate StartDate => Schedule.StartDate.PlusWeeks(Week);
        public LocalDate EndDate => StartDate.PlusDays(6);

        internal WeekSchedule(Schedule schedule, int week)
        {
            Check.If(schedule).IsNotNull();
            Check.If(week).IsGreaterOrEqual(0);

            Schedule = schedule;
            Week = week;
        }
    }
}

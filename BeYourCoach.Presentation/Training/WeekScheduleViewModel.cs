using BeYourCoach.Domain.Training;
using Conditions.Guards;
using Newtonsoft.Json;
using NodaTime;

namespace BeYourCoach.Presentation.Training
{
    public class WeekScheduleViewModel
    {
        [JsonIgnore]
        public Schedule Schedule { get; private set; }
        public int Week { get; private set; }
        public int WeekOfWeekYear => StartDate.WeekOfWeekYear;
        public LocalDate StartDate => Schedule.StartDate.PlusWeeks(Week);
        public LocalDate EndDate => StartDate.PlusDays(6);

        public WeekScheduleViewModel(Schedule schedule, int week)
        {
            Check.If(schedule).IsNotNull();
            Check.If(week).IsGreaterOrEqual(0);

            Schedule = schedule;
            Week = week;
        }
    }
}

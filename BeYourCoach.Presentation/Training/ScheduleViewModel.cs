using System.Collections.Generic;
using System.Linq;
using BeYourCoach.Domain.Training;
using Conditions.Guards;
using Newtonsoft.Json;

namespace BeYourCoach.Presentation.Training
{
    public class ScheduleViewModel
    {
        [JsonIgnore]
        public Schedule Schedule { get; private set; }

        public ScheduleViewModel(Schedule schedule)
        {
            Check.If(schedule).IsNotNull();

            Schedule = schedule;
        }

        public ICollection<WeekScheduleViewModel> WeekSchedules => new int[52].Select((n, i) => new WeekScheduleViewModel(Schedule, i)).ToList();
    }
}

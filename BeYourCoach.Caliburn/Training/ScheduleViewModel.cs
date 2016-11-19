using System.Linq;
using BeYourCoach.Domain.Training;
using Caliburn.Micro;
using Conditions.Guards;
using Newtonsoft.Json;
using NodaTime;

namespace BeYourCoach.Caliburn.Training
{
    public class ScheduleViewModel : Conductor<WeekScheduleViewModel>.Collection.OneActive
    {
        [JsonIgnore]
        public Schedule Schedule { get; private set; }

        public ScheduleViewModel(Schedule schedule)
        {
            Check.If(schedule).IsNotNull();

            Schedule = schedule;
        }

        public string Name => Schedule.Name;

        protected override void OnActivate()
        {
            base.OnActivate();
            for (var w = 0; w < 52; w++)
            {
                ActivateItem(new WeekScheduleViewModel(Schedule, w));
            }
            ActiveItem = Items.SingleOrDefault(ws => ws.WeekOfWeekYear == SystemClock.Instance.Now.InUtc().WeekOfWeekYear);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using BeYourCoach.Domain.Training;
using Caliburn.Micro;
using Conditions.Guards;
using Newtonsoft.Json;

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
            SelectedWeekSchedule = WeekSchedules.ElementAt(0);
        }

        public string Name => Schedule.Name;

        private ICollection<WeekScheduleViewModel> _weekSchedules;

        public ICollection<WeekScheduleViewModel> WeekSchedules
        {
            get { return _weekSchedules ?? (_weekSchedules = new int[52].Select((n, i) => new WeekScheduleViewModel(Schedule, i)).ToList()); }   
        }

        public WeekScheduleViewModel SelectedWeekSchedule
        {
            get { return ActiveItem; }
            set { ActiveItem = value; }
        }
    }
}

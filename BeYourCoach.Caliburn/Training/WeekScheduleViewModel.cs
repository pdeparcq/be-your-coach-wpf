using System.Collections.Generic;
using System.Linq;
using BeYourCoach.Domain.Training;
using Caliburn.Micro;
using NodaTime;

namespace BeYourCoach.Caliburn.Training
{
    public class WeekScheduleViewModel : Conductor<DayScheduleViewModel>.Collection.AllActive
    {
        public Schedule Schedule { get; }
        public int Week { get; }
        public int WeekOfWeekYear => StartDate.WeekOfWeekYear;
        public LocalDate StartDate => Schedule.StartDate.PlusWeeks(Week);
        public LocalDate EndDate => StartDate.PlusDays(6);
        public string Title => $"Week {WeekOfWeekYear} {StartDate.Year}";

        public WeekScheduleViewModel(Schedule schedule, int week)
        {
            Schedule = schedule;
            Week = week;

            Items.Add(Monday = new DayScheduleViewModel(Schedule, Week, IsoDayOfWeek.Monday));
            Items.Add(Tuesday = new DayScheduleViewModel(Schedule, Week, IsoDayOfWeek.Tuesday));
            Items.Add(Wednesday = new DayScheduleViewModel(Schedule, Week, IsoDayOfWeek.Wednesday));
            Items.Add(Thursday = new DayScheduleViewModel(Schedule, Week, IsoDayOfWeek.Thursday));
            Items.Add(Friday = new DayScheduleViewModel(Schedule, Week, IsoDayOfWeek.Friday));
            Items.Add(Saturday = new DayScheduleViewModel(Schedule, Week, IsoDayOfWeek.Saturday));
            Items.Add(Sunday = new DayScheduleViewModel(Schedule, Week, IsoDayOfWeek.Sunday));

            Schedule.TrainingScheduled += (sender, scheduled) =>
            {
                if (scheduled.Week != Week) return;
                NotifyOfPropertyChange(() => Trainings);
            };
        }

        public DayScheduleViewModel Monday { get; private set; }
        public DayScheduleViewModel Tuesday { get; private set; }
        public DayScheduleViewModel Wednesday { get; private set; }
        public DayScheduleViewModel Thursday { get; private set; }
        public DayScheduleViewModel Friday { get; private set; }
        public DayScheduleViewModel Saturday { get; private set; }
        public DayScheduleViewModel Sunday { get; private set; }

        public IEnumerable<Domain.Training.Training> Trainings => Schedule.Trainings.OrderBy(t => t.Discipline).Where(t => t.Week == Week);
    }
}

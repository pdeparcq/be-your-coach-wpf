using System;
using System.Collections.Generic;
using System.Linq;
using BeYourCoach.Domain.Training;
using BeYourCoach.Domain.Training.Events;
using Caliburn.Micro;
using Deparcq.Common.Domain;
using NodaTime;

namespace BeYourCoach.Caliburn.Training
{
    public class WeekScheduleViewModel : Conductor<DayScheduleViewModel>.Collection.AllActive, Deparcq.Common.Domain.IHandle<TrainingScheduled>
    {
        public Schedule Schedule { get; }
        public int Week { get; }
        public int WeekOfWeekYear => StartDate.WeekOfWeekYear;
        public LocalDate StartDate => Schedule.StartDate.PlusWeeks(Week);
        public LocalDate EndDate => StartDate.PlusDays(6);
        public string Title => $"Week {WeekOfWeekYear} {StartDate.Year}";
        public bool IsThisWeek => WeekOfWeekYear == SystemClock.Instance.Now.InUtc().WeekOfWeekYear;

        public WeekScheduleViewModel(Schedule schedule, int week)
        {
            IoC.Get<IDomainEventPublisher>().Subscribe(this);
            Schedule = schedule;
            Week = week;

            Items.Add(Monday = new DayScheduleViewModel(Schedule, Week, IsoDayOfWeek.Monday));
            Items.Add(Tuesday = new DayScheduleViewModel(Schedule, Week, IsoDayOfWeek.Tuesday));
            Items.Add(Wednesday = new DayScheduleViewModel(Schedule, Week, IsoDayOfWeek.Wednesday));
            Items.Add(Thursday = new DayScheduleViewModel(Schedule, Week, IsoDayOfWeek.Thursday));
            Items.Add(Friday = new DayScheduleViewModel(Schedule, Week, IsoDayOfWeek.Friday));
            Items.Add(Saturday = new DayScheduleViewModel(Schedule, Week, IsoDayOfWeek.Saturday));
            Items.Add(Sunday = new DayScheduleViewModel(Schedule, Week, IsoDayOfWeek.Sunday));
        }

        public DayScheduleViewModel Monday { get; private set; }
        public DayScheduleViewModel Tuesday { get; private set; }
        public DayScheduleViewModel Wednesday { get; private set; }
        public DayScheduleViewModel Thursday { get; private set; }
        public DayScheduleViewModel Friday { get; private set; }
        public DayScheduleViewModel Saturday { get; private set; }
        public DayScheduleViewModel Sunday { get; private set; }

        public IEnumerable<Domain.Training.Training> Trainings => Schedule.Trainings.OrderBy(t => t.Discipline).Where(t => t.Week == Week);

        public void Handle(TrainingScheduled e)
        {
            if (e.Week != Week) return;
            NotifyOfPropertyChange(() => Trainings);
        }
    }
}

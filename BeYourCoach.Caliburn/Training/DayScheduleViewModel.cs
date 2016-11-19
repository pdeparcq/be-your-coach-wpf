using System.Collections.Generic;
using System.Linq;
using BeYourCoach.Domain.Training;
using Caliburn.Micro;
using NodaTime;

namespace BeYourCoach.Caliburn.Training
{
    public class DayScheduleViewModel : PropertyChangedBase
    {
        public Schedule Schedule { get; private set; }
        public int Week { get; private set; }
        public IsoDayOfWeek DayOfWeek { get; private set; }
        public LocalDate Date => LocalDate.FromWeekYearWeekAndDay(Schedule.StartDate.PlusWeeks(Week).Year, Schedule.StartDate.PlusWeeks(Week).WeekOfWeekYear, DayOfWeek);

        public DayScheduleViewModel(Schedule schedule, int week, IsoDayOfWeek dayOfWeek)
        {
            Schedule = schedule;
            Week = week;
            DayOfWeek = dayOfWeek;
        }

        public void AddSwimTraining()
        {
            AddTraining(Discipline.Swimming);
        }

        public void AddCycleTraining()
        {
            AddTraining(Discipline.Cycling);
        }

        public void AddRunTraining()
        {
            AddTraining(Discipline.Running);
        }

        private void AddTraining(Discipline discipline)
        {
            Schedule.ScheduleTraining(Week, DayOfWeek, discipline);
            NotifyOfPropertyChange(() => Trainings);
        }

        public ICollection<DayTrainingViewModel> Trainings
        {
            get { return Schedule.Trainings.Where(t => t.Week == Week && t.DayOfWeek == DayOfWeek).Select(t => new DayTrainingViewModel(t)).ToList(); }
        }
    }
}

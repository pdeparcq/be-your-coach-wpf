using System.Linq;
using BeYourCoach.Caliburn.Training;
using BeYourCoach.Domain.Registration;
using BeYourCoach.Domain.Training;

using Caliburn.Micro;
using NodaTime;

namespace BeYourCoach.Caliburn
{
    public class ShellViewModel : Conductor<ScheduleViewModel>.Collection.OneActive
    {
        private readonly IScheduleRepository _scheduleRepository;

        public ShellViewModel(IScheduleRepository repository)
        {
            _scheduleRepository = repository;
        }
        protected override void OnActivate()
        {
            base.OnActivate();

            var schedule = _scheduleRepository.Schedules.FirstOrDefault();
            if (schedule == null)
            {
                schedule = new Schedule(new Athlete(new FullName("Pieter", "Deparcq")), "Jaarplan Pieter", new LocalDate(2016, 10, 31));
                _scheduleRepository.Add(schedule);
            }
            ActiveItem = new ScheduleViewModel(schedule);
            DisplayName = "BeYourCoach";
        }

        protected override void OnDeactivate(bool close)
        {
            _scheduleRepository.Update(ActiveItem.Schedule);
            base.OnDeactivate(close);
        }
    }
}

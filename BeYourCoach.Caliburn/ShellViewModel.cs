using System;
using System.Linq;
using BeYourCoach.Application.Training;
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
        private readonly ISchedulingService _schedulingService;

        public ShellViewModel(ISchedulingService service, IScheduleRepository repository)
        {
            _scheduleRepository = repository;
            _schedulingService = service;
        }
        protected override void OnActivate()
        {
            base.OnActivate();

            var schedule = _scheduleRepository.Schedules.FirstOrDefault() ?? _schedulingService.CreateSchedule(Guid.Empty, "Jaarplan Pieter", new LocalDate(2016, 10, 31));
            ActiveItem = new ScheduleViewModel(schedule);
            DisplayName = "BeYourCoach";
        }
    }
}

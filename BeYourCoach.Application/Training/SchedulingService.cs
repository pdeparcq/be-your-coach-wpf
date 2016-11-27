using System;
using System.Linq;
using BeYourCoach.Domain.Registration;
using BeYourCoach.Domain.Training;
using Deparcq.Common.Application;
using NodaTime;

namespace BeYourCoach.Application.Training
{
    public class SchedulingService : ApplicationServiceBase, ISchedulingService
    {
        protected IAthleteRepository AthleteRepository { get; private set; }
        protected IScheduleRepository ScheduleRepository { get; private set; }
        
        public SchedulingService(IUnitOfWork unitOfWork, IAthleteRepository athleteRepository, IScheduleRepository scheduleRepository) : base(unitOfWork)
        {
            AthleteRepository = athleteRepository;
            ScheduleRepository = scheduleRepository;
        }

        public Schedule CreateSchedule(Guid athleteId, string name, LocalDate startDate)
        {
            return InUnitOfWork(() =>
            {
                var schedule = ScheduleRepository.Add(new Schedule(AthleteRepository.Get(athleteId), name, startDate));
                return schedule;
            });

        }

        public Domain.Training.Training ScheduleTraining(Guid scheduleId, int week, IsoDayOfWeek dayOfWeek, Discipline discipline)
        {
            return InUnitOfWork(() =>
            {
                var schedule = ScheduleRepository.Get(scheduleId);
                var training = schedule.ScheduleTraining(week, dayOfWeek, discipline);
                ScheduleRepository.Update(schedule);
                return training;
            });
        }

        public Domain.Training.Training PlanTraining(Guid scheduleId, Guid trainingId, string description)
        {
            return InUnitOfWork(() =>
            {
                var schedule = ScheduleRepository.Get(scheduleId);
                var training = schedule.Trainings.Single(t => t.Id == trainingId);
                training.Plan(description);
                ScheduleRepository.Update(schedule);
                return training;
            });
        }

        public void RemoveTraining(Guid scheduleId, Guid trainingId)
        {
            InUnitOfWork(() =>
            {
                var schedule = ScheduleRepository.Get(scheduleId);
                schedule.RemoveTraining(trainingId);
                ScheduleRepository.Update(schedule);
            });
        }
    }
}

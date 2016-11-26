﻿using System;
using BeYourCoach.Domain.Registration;
using BeYourCoach.Domain.Training;
using Common.Domain;
using NodaTime;

namespace BeYourCoach.Application.Training
{
    public class SchedulingService : ISchedulingService
    {
        protected IUnitOfWork UnitOfWork { get; private set; }
        protected IAthleteRepository AthleteRepository { get; private set; }
        protected IScheduleRepository ScheduleRepository { get; private set; }
        
        public SchedulingService(IUnitOfWork unitOfWork, IAthleteRepository athleteRepository, IScheduleRepository scheduleRepository)
        {
            UnitOfWork = unitOfWork;
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

        protected T InUnitOfWork<T>(Func<T> execute)
        {
            try
            {
                var result = execute();
                UnitOfWork.Commit();
                return result;
            }
            catch (Exception)
            {
                UnitOfWork.Rollback();
                throw;
            }
        }
    }
}

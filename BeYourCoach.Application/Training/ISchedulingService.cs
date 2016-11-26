using System;
using BeYourCoach.Domain.Training;
using NodaTime;

namespace BeYourCoach.Application.Training
{
    public interface ISchedulingService {
        Schedule CreateSchedule(Guid athleteId, string name, LocalDate startDate);
        Domain.Training.Training ScheduleTraining(Guid scheduleId, int week, IsoDayOfWeek dayOfWeek, Discipline discipline);
    }
}
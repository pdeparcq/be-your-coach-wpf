﻿using System;
using BeYourCoach.Domain.Training;
using NodaTime;

namespace BeYourCoach.Application.Training
{
    public interface ISchedulingService {
        Schedule CreateSchedule(Guid athleteId, string name, LocalDate startDate);
        Domain.Training.Training ScheduleTraining(Guid scheduleId, int week, IsoDayOfWeek dayOfWeek, Discipline discipline);
        Domain.Training.Training PlanTraining(Guid scheduleId, Guid trainingId, string description);
        void RemoveTraining(Guid scheduleId, Guid trainingId);
        Domain.Training.Training MarkTrainingAsDone(Guid scheduleId, Guid trainingId, string remarks);
        Domain.Training.Training CancelTraining(Guid scheduleId, Guid trainingId, string remarks);
    }
}
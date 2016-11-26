using System;
using Common.Domain;
using Conditions.Guards;

namespace BeYourCoach.Domain.Training.Events
{
    public class TrainingScheduled : IDomainEvent
    {
        public Guid ScheduleId { get; private set; }
        public Guid TrainingId { get; private set; }
        public int Week { get; private set; }

        public TrainingScheduled(Schedule schedule, Training training)
        {
            Check.If(schedule).IsNotNull();
            Check.If(training).IsNotNull();

            ScheduleId = schedule.Id;
            TrainingId = training.Id;
            Week = training.Week;
        }
    }
}

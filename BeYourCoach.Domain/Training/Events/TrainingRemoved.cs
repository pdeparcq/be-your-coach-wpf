using System;
using Conditions.Guards;
using Deparcq.Common.Domain;

namespace BeYourCoach.Domain.Training.Events
{
    public class TrainingRemoved : IDomainEvent
    {
        public Guid ScheduleId { get; private set; }
        public Guid TrainingId { get; private set; }
        public int Week { get; private set; }

        public TrainingRemoved(Schedule schedule, Training training)
        {
            Check.If(schedule).IsNotNull();
            Check.If(training).IsNotNull();

            ScheduleId = schedule.Id;
            TrainingId = training.Id;
            Week = training.Week;
        }
    }
}

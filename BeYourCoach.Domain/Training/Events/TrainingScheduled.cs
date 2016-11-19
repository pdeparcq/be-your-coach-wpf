using System;

namespace BeYourCoach.Domain.Training.Events
{
    public class TrainingScheduled
    {
        public int Week { get; private set; }

        public TrainingScheduled(int week)
        {
            Week = week;
        }
    }
}

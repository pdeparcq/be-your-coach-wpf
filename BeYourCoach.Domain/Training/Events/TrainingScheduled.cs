namespace BeYourCoach.Domain.Training.Events
{
    public class TrainingScheduled : TrainingEvent
    {
        public TrainingScheduled(Schedule schedule, Training training) : base(schedule, training)
        {}
    }
}

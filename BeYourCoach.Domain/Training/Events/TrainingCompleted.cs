namespace BeYourCoach.Domain.Training.Events
{
    public class TrainingCompleted : TrainingEvent
    {
        public TrainingCompleted(Schedule schedule, Training training) : base(schedule, training) {}
    }
}

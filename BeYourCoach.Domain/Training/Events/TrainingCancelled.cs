namespace BeYourCoach.Domain.Training.Events
{
    public class TrainingCancelled : TrainingEvent
    {
        public TrainingCancelled(Schedule schedule, Training training) : base(schedule, training) {}
    }
}

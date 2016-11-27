namespace BeYourCoach.Domain.Training.Events
{
    public class TrainingRemoved : TrainingEvent {
        public TrainingRemoved(Schedule schedule, Training training) : base(schedule, training) {}
    }
    
}

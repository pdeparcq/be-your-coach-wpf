namespace BeYourCoach.Domain.Training.Events
{
    public class TrainingPlanned : TrainingEvent
    {
        public TrainingPlanned(Schedule schedule, Training training) : base(schedule, training) {}
    }
}

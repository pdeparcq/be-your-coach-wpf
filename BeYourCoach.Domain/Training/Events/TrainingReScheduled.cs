namespace BeYourCoach.Domain.Training.Events
{
    public class TrainingReScheduled : TrainingEvent
    {
        public TrainingReScheduled(Schedule schedule, Training training) : base(schedule, training) {}
    }
}

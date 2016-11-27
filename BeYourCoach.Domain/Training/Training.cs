using System;
using Conditions.Guards;
using Newtonsoft.Json;
using NodaTime;

namespace BeYourCoach.Domain.Training
{
    public enum Discipline
    {
        Swimming,
        Cycling,
        Running
    }

    public enum TrainingStatus
    {
        Created,
        Planned,
        Done,
        Cancelled
    }

    public class Training
    {
        public Guid Id { get; private set; }
        public int Week { get; private set; }
        public IsoDayOfWeek DayOfWeek { get; private set; }
        public Discipline Discipline { get; private set; }
        public TrainingStatus Status { get; private set; }
        public string Description { get; private set; }
        public string Remarks { get; private set; }

        [JsonConstructor]
        protected Training() { }

        internal Training(int week, IsoDayOfWeek dayOfWeek, Discipline discipline)
        {
            Check.If(week).IsGreaterOrEqual(0);

            Id = Guid.NewGuid();
            Week = week;
            DayOfWeek = dayOfWeek;
            Discipline = discipline;
        }

        public void Plan(string description)
        {
            Check.If(description).IsNotNullOrEmpty();

            if(Status != TrainingStatus.Created && Status != TrainingStatus.Planned)
                throw new ApplicationException("Can not plan training at this moment");
            
            Status = TrainingStatus.Planned;
            Description = description;
        }

        public void Done(string remarks)
        {
            if (Status != TrainingStatus.Planned && Status != TrainingStatus.Done)
                throw new ApplicationException("Can not mark training as done at this moment");

            Status = TrainingStatus.Done;
            Remarks = remarks;
        }

        public void Cancel(string remarks)
        {
            if (Status != TrainingStatus.Planned && Status != TrainingStatus.Done)
                throw new ApplicationException("Can not cancel training at this moment");

            Status = TrainingStatus.Cancelled;
            Remarks = remarks;
        }

        public void ReSchedule(int week, IsoDayOfWeek dayOfWeek)
        {
            Check.If(week).IsGreaterOrEqual(0);

            Week = week;
            DayOfWeek = dayOfWeek;
        }
    }
}

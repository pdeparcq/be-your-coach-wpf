using System;
using BeYourCoach.Domain.Registration;
using Conditions.Guards;
using NodaTime;

namespace BeYourCoach.Domain.Training
{
    public class Schedule
    {
        public Guid Id { get; private set; }
        public Guid AthleteId { get; private set; }
        public string Name { get; private set; }
        public LocalDate StartDate { get; private set; }

        public Schedule(Athlete athlete, string name, LocalDate startDate)
        {
            Check.If(athlete).IsNotNull();
            Check.If(name).IsNotNullOrEmpty();

            if(startDate.IsoDayOfWeek != IsoDayOfWeek.Monday)
                throw new ArgumentException("Schedule should start on Monday", nameof(startDate));

            Id = Guid.NewGuid();
            AthleteId = athlete.Id;
            Name = name;
            StartDate = startDate;
        }
    }
}

using System;
using System.Collections.Generic;
using BeYourCoach.Domain.Registration;
using BeYourCoach.Domain.Training.Events;
using Conditions.Guards;
using Newtonsoft.Json;
using NodaTime;

namespace BeYourCoach.Domain.Training
{
    public class Schedule
    {
        public Guid Id { get; private set; }
        public Guid AthleteId { get; private set; }
        public string Name { get; private set; }
        public LocalDate StartDate { get; private set; }
        public ICollection<Training> Trainings { get; private set; }

        public event EventHandler<TrainingScheduled> TrainingScheduled;

        [JsonConstructor]
        protected Schedule() { }

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
            Trainings = new List<Training>();
        }

        public Training ScheduleTraining(int week, IsoDayOfWeek dayOfWeek, Discipline discipline)
        {
            var training = new Training(week , dayOfWeek, discipline);
            Trainings.Add(training);
            TrainingScheduled?.Invoke(this, new TrainingScheduled(week));
            return training;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using BeYourCoach.Domain.Registration;
using BeYourCoach.Domain.Training.Events;
using Conditions.Guards;
using Deparcq.Common.Domain;
using Newtonsoft.Json;
using NodaTime;

namespace BeYourCoach.Domain.Training
{
    public class Schedule : EntityBase
    {
        public Guid AthleteId { get; private set; }
        public string Name { get; private set; }
        public LocalDate StartDate { get; private set; }
        public ICollection<Training> Trainings { get; private set; }

        [JsonConstructor]
        protected Schedule() { }

        public Schedule(Athlete athlete, string name, LocalDate startDate)
        {
            Check.If(athlete).IsNotNull();
            Check.If(name).IsNotNullOrEmpty();

            if(startDate.IsoDayOfWeek != IsoDayOfWeek.Monday)
                throw new ArgumentException("Schedule should start on Monday", nameof(startDate));

            AthleteId = athlete.Id;
            Name = name;
            StartDate = startDate;
            Trainings = new List<Training>();
        }

        public Training ScheduleTraining(int week, IsoDayOfWeek dayOfWeek, Discipline discipline)
        {
            var training = new Training(week , dayOfWeek, discipline);
            Trainings.Add(training);
            Events.Add(new TrainingScheduled(this, training));
            return training;
        }

        public void RemoveTraining(Guid trainingId)
        {
            var training = Trainings.Single(t => t.Id == trainingId);
            Trainings.Remove(training);
            Events.Add(new TrainingRemoved(this, training));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using BeYourCoach.Domain.Registration;
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

        public WeekSchedule GetWeekSchedule(int week)
        {
            return new WeekSchedule(this, week);
        }

        public Training ScheduleTraining(int week, IsoDayOfWeek dayOfWeek, string discipline)
        {
            var training = new Training(GetWeekSchedule(week), dayOfWeek, discipline);
            Trainings.Add(training);
            return training;
        }

        public Training ReScheduleTraining(Guid trainingId, int week, IsoDayOfWeek dayOfWeek)
        {
            var training = Trainings.Single(t => t.Id == trainingId);
            training.ReSchedule(GetWeekSchedule(week), dayOfWeek);
            return training;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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
        public ICollection<WeekSchedule> WeekSchedules { get; private set; }
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
            WeekSchedules = new List<WeekSchedule>();
            Trainings = new List<Training>();
        }

        public WeekSchedule ScheduleWeek(int week)
        {
            if (WeekSchedules.Any(w => w.Week == week))
            {
                throw new ArgumentException($"Week {week} already scheduled", nameof(week));
            }
            var weekSchedule = new WeekSchedule(this, week);
            WeekSchedules.Add(weekSchedule);
            return weekSchedule;
        }

        public WeekSchedule GetWeekSchedule(int week)
        {
            return WeekSchedules.SingleOrDefault(ws => ws.Week == week);
        }

        public Training ScheduleTraining(int week, IsoDayOfWeek dayOfWeek, string discipline)
        {
            var weekSchedule = GetWeekSchedule(week) ?? ScheduleWeek(week);
            var training = new Training(weekSchedule, dayOfWeek, discipline);
            Trainings.Add(training);
            return training;
        }

        public Training ReScheduleTraining(Guid trainingId, int week, IsoDayOfWeek dayOfWeek)
        {
            var training = Trainings.Single(t => t.Id == trainingId);
            var weekSchedule = GetWeekSchedule(week) ?? ScheduleWeek(week);
            training.ReSchedule(weekSchedule, dayOfWeek);
            return training;
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            foreach (var weekSchedule in WeekSchedules)
            {
                weekSchedule.Schedule = this;
            }
        }
    }
}

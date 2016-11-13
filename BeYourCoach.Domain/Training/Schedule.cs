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

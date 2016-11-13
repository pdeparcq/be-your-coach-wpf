using System;
using Conditions.Guards;
using Newtonsoft.Json;
using NodaTime;

namespace BeYourCoach.Domain.Training
{
    public class Training
    {
        public Guid Id { get; private set; }
        public int Week { get; private set; }
        public IsoDayOfWeek DayOfWeek { get; private set; }
        public string Discipline { get; private set; }

        [JsonConstructor]
        protected Training() { }

        internal Training(WeekSchedule weekSchedule, IsoDayOfWeek dayOfWeek, string discipline)
        {
            Check.If(weekSchedule).IsNotNull();
            Check.If(discipline).IsNotNullOrEmpty();

            Id = Guid.NewGuid();
            Week = weekSchedule.Week;
            DayOfWeek = dayOfWeek;
            Discipline = discipline;
        }

        internal void ReSchedule(WeekSchedule weekSchedule, IsoDayOfWeek dayOfWeek)
        {
            Check.If(weekSchedule).IsNotNull();

            Week = weekSchedule.Week;
            DayOfWeek = dayOfWeek;
        }
    }
}

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

    public class Training
    {
        public Guid Id { get; private set; }
        public int Week { get; private set; }
        public IsoDayOfWeek DayOfWeek { get; private set; }
        public Discipline Discipline { get; private set; }

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

        public void ReSchedule(int week, IsoDayOfWeek dayOfWeek)
        {
            Check.If(week).IsGreaterOrEqual(0);

            Week = week;
            DayOfWeek = dayOfWeek;
        }
    }
}

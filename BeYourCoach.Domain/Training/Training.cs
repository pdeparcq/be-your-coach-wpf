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

        internal Training(int week, IsoDayOfWeek dayOfWeek, string discipline)
        {
            Check.If(week).IsGreaterOrEqual(0);
            Check.If(discipline).IsNotNullOrEmpty();

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

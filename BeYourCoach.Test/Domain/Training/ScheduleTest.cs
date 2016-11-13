using System;
using System.Linq;
using BeYourCoach.Common.Json;
using BeYourCoach.Domain.Registration;
using BeYourCoach.Domain.Training;
using NodaTime;
using NUnit.Framework;

namespace BeYourCoach.Test.Domain.Training
{
    [TestFixture]
    public class ScheduleTest
    {
        [Test]
        public void CanCreateSchedule()
        {
            var schedule = new Schedule(new Athlete(new FullName("Pieter", "Deparcq")), "Jaarplan Pieter", new LocalDate(2016, 10, 31));
            schedule.PrettyPrint();
        }

        [Test]
        public void CanNotCreateScheduleOnWrongDay()
        {
            Assert.Throws<ArgumentException>(() => new Schedule(new Athlete(new FullName("Pieter", "Deparcq")), "Jaarplan Pieter", new LocalDate(2016, 11, 1)));
        }

        [Test]
        public void CanCreateWeekSchedules()
        {
            var schedule = new Schedule(new Athlete(new FullName("Pieter", "Deparcq")), "Jaarplan Pieter", new LocalDate(2016, 10, 31));
            5.Times(w => schedule.ScheduleWeek(w));
            schedule.PrettyPrint();
        }

        [Test]
        public void CanSerializeAndDeserializeToAndFromJson()
        {
           
            var schedule = new Schedule(new Athlete(new FullName("Pieter", "Deparcq")), "Jaarplan Pieter", new LocalDate(2016, 10, 31));
            5.Times(w => schedule.ScheduleWeek(w));
            schedule = schedule.Serialize().Deserialize<Schedule>();

            Assert.IsNotNull(schedule);
            Assert.AreEqual("Jaarplan Pieter", schedule.Name);
            Assert.AreEqual(5, schedule.WeekSchedules.Count);
            Assert.IsNotNull(schedule.WeekSchedules.First().Schedule);
        }
    }
}

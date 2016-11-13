using System;
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
    }
}

﻿using System;
using BeYourCoach.Domain.Registration;
using BeYourCoach.Domain.Training;
using Deparcq.Common.Json;
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
        public void CanScheduleAndReScheduleTrainings()
        {
            var schedule = new Schedule(new Athlete(new FullName("Pieter", "Deparcq")), "Jaarplan Pieter", new LocalDate(2016, 10, 31));
            schedule.ScheduleTraining(0, IsoDayOfWeek.Monday, Discipline.Running);
            schedule.ScheduleTraining(0, IsoDayOfWeek.Thursday, Discipline.Cycling);
            var training = schedule.ScheduleTraining(3, IsoDayOfWeek.Saturday, Discipline.Swimming);
            training.ReSchedule(2, IsoDayOfWeek.Sunday);
            schedule.PrettyPrint();
        }

        [Test]
        public void CanSerializeAndDeserializeToAndFromJson()
        {
            var schedule = CreateTestSchedule();
            schedule = schedule.Serialize().Deserialize<Schedule>();

            Assert.IsNotNull(schedule);
            Assert.AreEqual("Jaarplan Pieter", schedule.Name);
            Assert.AreEqual(3, schedule.Trainings.Count);
        }

        public static Schedule CreateTestSchedule()
        {
            var schedule = new Schedule(new Athlete(new FullName("Pieter", "Deparcq")), "Jaarplan Pieter", new LocalDate(2016, 10, 31));
            schedule.ScheduleTraining(0, IsoDayOfWeek.Monday, Discipline.Running);
            schedule.ScheduleTraining(0, IsoDayOfWeek.Thursday, Discipline.Cycling);
            schedule.ScheduleTraining(3, IsoDayOfWeek.Saturday, Discipline.Swimming);
            return schedule;
        }
    }
}

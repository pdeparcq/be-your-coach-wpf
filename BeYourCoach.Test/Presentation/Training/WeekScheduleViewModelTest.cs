using BeYourCoach.Domain.Registration;
using BeYourCoach.Domain.Training;
using BeYourCoach.Presentation.Training;
using NodaTime;
using NUnit.Framework;

namespace BeYourCoach.Test.Presentation.Training
{
    [TestFixture]
    public class WeekScheduleViewModelTest
    {
        [Test]
        public void CanCreateWeekScheduleViewModel()
        {
            var schedule = new Schedule(new Athlete(new FullName("Pieter", "Deparcq")), "Jaarplan Pieter", new LocalDate(2016, 10, 31));
            schedule.ScheduleTraining(0, IsoDayOfWeek.Monday, "running");
            schedule.ScheduleTraining(0, IsoDayOfWeek.Thursday, "cycling");
            schedule.ScheduleTraining(3, IsoDayOfWeek.Saturday, "swimming");

            var vm = new WeekScheduleViewModel(schedule, 1);

            Assert.AreEqual(45, vm.WeekOfWeekYear);
            Assert.AreEqual(1, vm.Week);
            Assert.AreEqual(7, vm.StartDate.Day);
            Assert.AreEqual(13, vm.EndDate.Day);

            vm.PrettyPrint();
        }
    }
}

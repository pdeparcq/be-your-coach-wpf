using BeYourCoach.Presentation.Training;
using BeYourCoach.Test.Domain.Training;
using NUnit.Framework;

namespace BeYourCoach.Test.Presentation.Training
{
    [TestFixture]
    public class WeekScheduleViewModelTest
    {
        [Test]
        public void CanCreateWeekScheduleViewModel()
        {
            var vm = new WeekScheduleViewModel(ScheduleTest.CreateTestSchedule(), 1);

            Assert.AreEqual(45, vm.WeekOfWeekYear);
            Assert.AreEqual(1, vm.Week);
            Assert.AreEqual(7, vm.StartDate.Day);
            Assert.AreEqual(13, vm.EndDate.Day);

            vm.PrettyPrint();
        }
    }
}

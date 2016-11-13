using BeYourCoach.Presentation.Training;
using BeYourCoach.Test.Domain.Training;
using NUnit.Framework;

namespace BeYourCoach.Test.Presentation.Training
{
    [TestFixture]
    public class ScheduleViewModelTest
    {
        [Test]
        public void CanCreateScheduleViewModel()
        {
            var vm = new ScheduleViewModel(ScheduleTest.CreateTestSchedule());
            vm.PrettyPrint();
        }
    }
}

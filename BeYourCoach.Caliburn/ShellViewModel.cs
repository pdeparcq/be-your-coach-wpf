using BeYourCoach.Caliburn.Training;
using BeYourCoach.Domain.Registration;
using BeYourCoach.Domain.Training;

using Caliburn.Micro;
using NodaTime;

namespace BeYourCoach.Caliburn
{
    public class ShellViewModel : Conductor<object>.Collection.OneActive
    {
        public ShellViewModel()
        {
            ActiveItem = new ScheduleViewModel(new Schedule(new Athlete(new FullName("Pieter", "Deparcq")), "Jaarplan Pieter", new LocalDate(2016, 10, 31)));
        }
    }
}

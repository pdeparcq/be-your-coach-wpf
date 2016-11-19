using System.Windows.Media;
using BeYourCoach.Domain.Training;

namespace BeYourCoach.Caliburn.Training
{
    public class DayTrainingViewModel
    {
        public Domain.Training.Training Training { get; private set; }

        public DayTrainingViewModel(Domain.Training.Training training)
        {
            Training = training;
        }

        public Discipline Discipline => Training.Discipline;
    }
}

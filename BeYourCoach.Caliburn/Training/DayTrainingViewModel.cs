using System.Windows.Media;

namespace BeYourCoach.Caliburn.Training
{
    public class DayTrainingViewModel
    {
        public Domain.Training.Training Training { get; private set; }

        public DayTrainingViewModel(Domain.Training.Training training)
        {
            Training = training;
        }

        public string Discipline => Training.Discipline;

        public Brush DisciplineColor
        {
            get
            {
                switch (Discipline)
                {
                    case "running":
                        return Brushes.LightGreen;
                    case "swimming":
                        return Brushes.DeepSkyBlue;
                    case "cycling":
                        return Brushes.OrangeRed;
                    default:
                        return Brushes.Gray;
                }
            }
        }
    }
}

using System;
using BeYourCoach.Application.Training;
using BeYourCoach.Domain.Training;
using Caliburn.Micro;

namespace BeYourCoach.Caliburn.Training
{
    public class DayTrainingViewModel : PropertyChangedBase
    {
        public Schedule Schedule { get; private set; }
        public Domain.Training.Training Training { get; private set; }

        public DayTrainingViewModel(Schedule schedule, Domain.Training.Training training)
        {
            Schedule = schedule;
            Training = training;
        }

        public Discipline Discipline => Training.Discipline;

        private string _description;
        public string Description
        {
            get { return _description ?? Training.Description; }
            set
            {
                _description = value; 
                NotifyOfPropertyChange(() => CanPlanTraining);
            }
        }

        public string Remarks => Training.Remarks;

        public bool RemarksIsVisible => Training.Status == TrainingStatus.Planned;

        public string Status => Enum.GetName(typeof(TrainingStatus), Training.Status);

        public bool CanPlanTraining => Training.Status == TrainingStatus.Created && !string.IsNullOrEmpty(Description);

        public bool PlanTrainingIsVisible => Training.Status == TrainingStatus.Created;


        public void RemoveTraining()
        {
            var service = IoC.Get<ISchedulingService>();
            service.RemoveTraining(Schedule.Id, Training.Id);
        }

        public void PlanTraining(string description)
        {
            var service = IoC.Get<ISchedulingService>();
            service.PlanTraining(Schedule.Id, Training.Id, description);
            NotifyOfPropertyChange(() => Status);
            NotifyOfPropertyChange(() => CanPlanTraining);
            NotifyOfPropertyChange(() => PlanTrainingIsVisible);
            NotifyOfPropertyChange(() => RemarksIsVisible);
        }
    }
}

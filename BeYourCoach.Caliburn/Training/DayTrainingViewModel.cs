using System;
using BeYourCoach.Application.Training;
using BeYourCoach.Domain.Training;
using Caliburn.Micro;
using NodaTime;

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

        public bool IsInFuture
        {
            get
            {
                var date = LocalDate.FromWeekYearWeekAndDay(Schedule.StartDate.PlusWeeks(Training.Week).Year, Schedule.StartDate.PlusWeeks(Training.Week).WeekOfWeekYear, Training.DayOfWeek);
                return date.AtMidnight() >= SystemClock.Instance.Now.InUtc().Date.AtMidnight();
            }
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

        public bool DescriptionIsReadOnly => Training.Status != TrainingStatus.Created;

        private string _remarks;
        public string Remarks
        {
            get { return _remarks ?? Training.Remarks; }
            set
            {
                _remarks = value;
                NotifyOfPropertyChange(() => CanMarkTrainingAsDone);
                NotifyOfPropertyChange(() => CanCancelTraining);
            }
        }

        public bool RemarksIsVisible => Training.Status != TrainingStatus.Created;

        public string Status => Enum.GetName(typeof(TrainingStatus), Training.Status);



        public bool PlanTrainingIsVisible => Training.Status == TrainingStatus.Created;
        public bool CanPlanTraining => PlanTrainingIsVisible && !string.IsNullOrEmpty(Description);

        public void PlanTraining(string description)
        {
            var service = IoC.Get<ISchedulingService>();
            service.PlanTraining(Schedule.Id, Training.Id, description);
            NotifyOfPropertyChange("");
        }

        public bool CancelTrainingIsVisible => Training.Status == TrainingStatus.Planned;
        public bool CanCancelTraining => CancelTrainingIsVisible && !string.IsNullOrEmpty(Remarks);

        public void CancelTraining(string remarks)
        {
            var service = IoC.Get<ISchedulingService>();
            service.CancelTraining(Schedule.Id, Training.Id, remarks);
            NotifyOfPropertyChange("");
        }

        public bool MarkTrainingAsDoneIsVisible => Training.Status == TrainingStatus.Planned && !IsInFuture;
        public bool CanMarkTrainingAsDone => MarkTrainingAsDoneIsVisible && !string.IsNullOrEmpty(Remarks);

        public void MarkTrainingAsDone(string remarks)
        {
            var service = IoC.Get<ISchedulingService>();
            service.MarkTrainingAsDone(Schedule.Id, Training.Id, remarks);
            NotifyOfPropertyChange("");
        }

        public bool RemoveTrainingIsVisible => Training.Status == TrainingStatus.Created;
        public void RemoveTraining()
        {
            var service = IoC.Get<ISchedulingService>();
            service.RemoveTraining(Schedule.Id, Training.Id);
        }
    }
}

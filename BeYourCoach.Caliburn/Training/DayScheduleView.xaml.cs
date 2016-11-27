using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Deparcq.Common.Json;

namespace BeYourCoach.Caliburn.Training
{
    /// <summary>
    /// Interaction logic for DayScheduleView.xaml
    /// </summary>
    public partial class DayScheduleView : UserControl
    {
        public DayScheduleView()
        {
            InitializeComponent();
        }

        private void Grid_OnDrop(object sender, DragEventArgs e)
        {
            var control = sender as Grid;
            var context = control?.DataContext as DayScheduleViewModel;
            if (context != null)
            {
                var training = (e.Data.GetData(DataFormats.StringFormat) as string).Deserialize<Domain.Training.Training>();
                if (training != null)
                {
                    context.ReScheduleTraining(training);
                }
            }
        }
    }
}

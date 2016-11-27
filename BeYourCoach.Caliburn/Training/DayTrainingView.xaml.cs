using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Deparcq.Common.Json;

namespace BeYourCoach.Caliburn.Training
{
    /// <summary>
    /// Interaction logic for DayTrainingView.xaml
    /// </summary>
    public partial class DayTrainingView : UserControl
    {
        public DayTrainingView()
        {
            InitializeComponent();
        }

        private void UIElement_OnMouseMove(object sender, MouseEventArgs e)
        {
            var control = sender as Border;
            var context = control?.DataContext as DayTrainingViewModel;
            if (context != null && e.LeftButton == MouseButtonState.Pressed)
            {
                Dispatcher.BeginInvoke((Action)(() =>
                {
                    try
                    {
                        DragDrop.DoDragDrop(control, context.Training.Serialize(), DragDropEffects.Move);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }));
            }
        }
    }
}

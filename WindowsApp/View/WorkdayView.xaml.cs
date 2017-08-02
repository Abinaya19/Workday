using System.Windows;

namespace Workday.View
{
    /// <summary>
    /// Interaction logic for WorkdayView.xaml
    /// </summary>
    public partial class WorkdayView : Window
    {
        public WorkdayView()
        {
            InitializeComponent();
            DataContext = new WorkdayViewModel();
        }
    }
}

using System.ComponentModel;
using System.Windows.Input;

namespace Workday
{
    public class WorkdayViewModel : INotifyPropertyChanged
    {
        private DelegateCommand _workdayCommand;
        private DelegateCommand _workingCommand;
        private bool _workdayStarted;
        private bool _working;

        public event PropertyChangedEventHandler PropertyChanged;

        public string WorkdayButtonContent
        {
            get
            {
                if (_workdayStarted)
                {
                    return Properties.Resources.WorkdayStop;
                }
                else
                {
                    return Properties.Resources.WorkdayStart;
                }
            }
        }

        public string WorkingButtonContent
        {
            get
            {
                if (_working)
                {
                    return Properties.Resources.WorkStop;
                }
                else
                {
                    return Properties.Resources.WorkStart;
                }
            }
        }

        public ICommand WorkdayCommand
        {
            get
            {
                return _workdayCommand;
            }
        }

        public ICommand WorkingCommand
        {
            get
            {
                return _workingCommand;
            }
        }

        public WorkdayViewModel()
        {
            _working = false;
            _workdayStarted = false;
            _workdayCommand = DelegateCommand.Create(StartEndWorkday);
            _workingCommand = DelegateCommand.Create(ToggleWorking, false);
        }

        private void StartEndWorkday()
        {
            _workdayStarted = !_workdayStarted;
            _workingCommand.CanExecuteValue = _workdayStarted;

            if (!_workdayStarted && _working)
            {
                ToggleWorking();
            }

            OnPropertyChanged(nameof(WorkdayButtonContent));
        }

        private void ToggleWorking()
        {
            _working = !_working;

            OnPropertyChanged(nameof(WorkingButtonContent));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

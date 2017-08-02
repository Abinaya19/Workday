using System.ComponentModel;
using System.Windows.Input;
using System;

namespace Workday
{
    public class WorkdayViewModel : INotifyPropertyChanged
    {
        private DelegateCommand _workdayCommand;
        private DelegateCommand _blockCommand;
        private WorkdayModel _model;
        private TimeSpan _blockTimeSpan;
        private TimeSpan _workdayTimeSpan;

        public event PropertyChangedEventHandler PropertyChanged;

        public string WorkdayButtonContent
        {
            get
            {
                if (_model.WorkdayStarted)
                {
                    return Properties.Resources.WorkdayStop;
                }
                else
                {
                    return Properties.Resources.WorkdayStart;
                }
            }
        }

        public string BlockButtonContent
        {
            get
            {
                if (_model.BlockStarted)
                {
                    return Properties.Resources.WorkStop;
                }
                else
                {
                    return Properties.Resources.WorkStart;
                }
            }
        }

        public TimeSpan BlockTimeSpan
        {
            get
            {
                return _blockTimeSpan;
            }
            set
            {
                if (value != _blockTimeSpan)
                {
                    _blockTimeSpan = value;

                    OnPropertyChanged(nameof(BlockTimeSpan));
                }
            }
        }

        public TimeSpan WorkdayTimeSpan
        {
            get
            {
                return _workdayTimeSpan;
            }
            set
            {
                if (value != _workdayTimeSpan)
                {
                    _workdayTimeSpan = value;

                    OnPropertyChanged(nameof(WorkdayTimeSpan));
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

        public ICommand BlockCommand
        {
            get
            {
                return _blockCommand;
            }
        }

        public WorkdayViewModel()
        {
            _blockTimeSpan = new TimeSpan();
            _workdayTimeSpan = new TimeSpan();
            _model = new WorkdayModel();
            _model.TimerExpired += (sender, args) =>
            {
                BlockTimeSpan = args.BlockTotal;
                WorkdayTimeSpan = args.WorkdayTotal;
            };

            _workdayCommand = DelegateCommand.Create(StartEndWorkday);
            _blockCommand = DelegateCommand.Create(StartEndBlock, false);
        }

        private void StartEndWorkday()
        {
            if (_model.WorkdayStarted)
            {
                _model.StopWorkday();
            }
            else
            {
                _model.StartWorkday();
            }

            _blockCommand.CanExecuteValue = _model.WorkdayStarted;

            OnPropertyChanged(nameof(WorkdayButtonContent));
            OnPropertyChanged(nameof(BlockButtonContent));
        }

        private void StartEndBlock()
        {
            if (_model.BlockStarted)
            {
                _model.StopBlock();
            }
            else
            {
                _model.StartBlock();
            }
        
            OnPropertyChanged(nameof(BlockButtonContent));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

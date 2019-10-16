using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ProgressBar
{
    class ViewModel : INotifyPropertyChanged
    {
        int _progress;
        public int Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Progress)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}

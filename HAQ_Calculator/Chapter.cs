using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HAQ_Calculator.Annotations;

namespace HAQ_Calculator
{
    public class Chapter : INotifyPropertyChanged
    {
        private int _totalPoints;
        private bool _isEnabled = true;
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

        public int TotalPoints
        {
            get => _totalPoints;
            set
            {
                _totalPoints = value;
                OnPropertyChanged();
            }
        }
        
        public List<int> QuestionsPoints { get; set; }
        public int AdditionalPoints { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
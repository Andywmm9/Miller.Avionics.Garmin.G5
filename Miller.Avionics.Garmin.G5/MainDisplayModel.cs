using System.ComponentModel;


namespace Miller.Avionics.Garmin.G5
{
    public class MainDisplayModel : INotifyPropertyChanged
    {
        private double _bankDegrees;
        private double _pitchDegrees;

        public event PropertyChangedEventHandler PropertyChanged;

        public double BankDegrees
        {
            get { return _bankDegrees; }
            set
            {
                _bankDegrees = value;
                OnPropertyChanged(nameof(BankDegrees));
            }
        }
        public double PitchDegrees
        {
            get { return _pitchDegrees; }
            set
            {
                _pitchDegrees = value;
                OnPropertyChanged(nameof(PitchDegrees));
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using System.Windows;


namespace Miller.Avionics.Garmin.G5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainDisplay : Window
    {
        public MainDisplay()
        {
            var mainDisplayModel = new MainDisplayModel();

            mainDisplayModel.BankDegrees = 45;
            mainDisplayModel.PitchDegrees = 20;
            DataContext = mainDisplayModel;

            InitializeComponent();
        }
    }
}

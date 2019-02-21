using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Miller.Avionics.Garmin.G5
{
    /// <summary>
    /// Interaction logic for MainWindowTest.xaml
    /// </summary>
    public partial class MainWindowTest : Window
    {
        public MainWindowTest()
        {
            DrawHorizon();

            var mainDisplayModel = new MainDisplayModel();

            mainDisplayModel.BankDegrees = 45;
            mainDisplayModel.PitchDegrees = 20;
            DataContext = mainDisplayModel;

            InitializeComponent();
        }

        private void DrawHorizon()
        {
            //var line = new Line();

            //line.HorizontalAlignment = HorizontalAlignment.Stretch;
            //line.Stroke = new SolidColorBrush(Colors.Black);
            //line.StrokeThickness = 2;
            //line.X1 = 50;
            //line.X2 = 500;

            //mCanvas.Children.Add(line);

            //var skyPolygon = new Polygon();

            //skyPolygon.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#466DEF"));
            //skyPolygon.Points.Add(new Point(0, 0));
            //skyPolygon.Points.Add(new Point(mCanvas.ActualWidth, 0));
            //skyPolygon.Points.Add(new Point(mCanvas.ActualWidth, 500));

            //mCanvas.Children.Add(skyPolygon);
        }
    }
}

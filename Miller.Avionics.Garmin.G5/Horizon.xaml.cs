using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System;

namespace Miller.Avionics.Garmin.G5
{
    /// <summary>
    /// Represents the horizon.
    /// </summary>
    public partial class Horizon : UserControl
    {
        public const string SkyStartColor = "#2F40B4";
        public const string SkyEndColor = "#5866AD";
        public const string GroundStartColor = "#6D4D1E";
        public const string GroundEndColor = "#523B1B";

        public Horizon()
        {
            InitializeComponent();
            InitializeGroundAndSkyPolygons();
            IntializeBankAndPitchMonitoring();

            SizeChanged += (s, e) => UpdateGroundAndSkyPolygonPoints();
        }

        /// <summary>
        /// Initializes the polygons for the ground and sky at straight and level flight.
        /// </summary>
        private void InitializeGroundAndSkyPolygons()
        {
            var skyStartColor = (Color)ColorConverter.ConvertFromString(SkyStartColor);
            var skyEndColor = (Color)ColorConverter.ConvertFromString(SkyEndColor);
            var groundStartColor = (Color)ColorConverter.ConvertFromString(GroundStartColor);
            var groundEndColor = (Color)ColorConverter.ConvertFromString(GroundEndColor);
            var skyBrush = new LinearGradientBrush(skyStartColor, skyEndColor, new Point(0, 0), new Point(0, 1));
            var groundBrush = new LinearGradientBrush(groundStartColor, groundEndColor, new Point(0, 0), new Point(0, 1));

            _skyPolygon.Fill = skyBrush;
            _groundPolygon.Fill = groundBrush;

            MainGrid.Children.Add(_skyPolygon);
            MainGrid.Children.Add(_groundPolygon);
        }

        private void IntializeBankAndPitchMonitoring()
        {
            _xPlaneDataListener.StartListening();

            _xPlaneDataListener.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(XPlaneDataListener.Pitch_Degrees)
                    || e.PropertyName == nameof(XPlaneDataListener.Role_Degrees))
                    Dispatcher.Invoke(new Action(() => UpdateGroundAndSkyPolygonPoints())); 
            };
        }

        private void UpdateGroundAndSkyPolygonPoints()
        {
            _skyPolygon.Points.Clear();
            _groundPolygon.Points.Clear();

            // Calculate how much we need to move the y axis from the bank of the aircraft.
            var bankYOffset = Math.Tan(-_xPlaneDataListener.Role_Degrees * (Math.PI / 180)) * ActualWidth;
            var pitchYOffset = Math.Tan(_xPlaneDataListener.Pitch_Degrees * (Math.PI / 180)) * ActualWidth;

            _skyPolygon.Points.Add(new Point(0, 0));
            _skyPolygon.Points.Add(new Point(ActualWidth, 0));
            _skyPolygon.Points.Add(new Point(ActualWidth, ActualHeight / 2 + bankYOffset / 2 + pitchYOffset)); // Split the difference between the left and right side so we aren't showing pitch.
            _skyPolygon.Points.Add(new Point(0, ActualHeight / 2 - bankYOffset / 2 + pitchYOffset));

            _groundPolygon.Points.Add(new Point(0, ActualHeight / 2 - bankYOffset / 2 + pitchYOffset));
            _groundPolygon.Points.Add(new Point(ActualWidth, ActualHeight / 2 + bankYOffset / 2 + pitchYOffset));
            _groundPolygon.Points.Add(new Point(ActualWidth, ActualHeight));
            _groundPolygon.Points.Add(new Point(0, ActualHeight));
        }

        private readonly Polygon _skyPolygon = new Polygon();
        private readonly Polygon _groundPolygon = new Polygon();
        private readonly XPlaneDataListener _xPlaneDataListener = new XPlaneDataListener();
    }
}

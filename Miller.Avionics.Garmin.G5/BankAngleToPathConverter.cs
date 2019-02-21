using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;

namespace Miller.Avionics.Garmin.G5
{
    public class BankAngleToPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var element = value as FrameworkElement;
            var horizonModel = element.DataContext as HorizonModel;
            var points = new PointCollection();
            var displayType = (DisplayTypes)parameter;

            Action fillPoints = () =>
            {
                var yOffset = Math.Tan(horizonModel.BankDegrees * (Math.PI / 180)) * element.ActualWidth;

                points.Clear();

                if (displayType == DisplayTypes.Sky)
                {
                    points.Add(new Point(0, 0));
                    points.Add(new Point(element.ActualWidth, 0));
                    points.Add(new Point(element.ActualWidth, (element.ActualHeight / 2) + (yOffset / 2)));
                    points.Add(new Point(0, (element.ActualHeight / 2) - yOffset / 2));
                } else
                {
                    points.Add(new Point(0, (element.ActualHeight / 2) - yOffset / 2));
                    points.Add(new Point(element.ActualWidth, (element.ActualHeight / 2) + (yOffset / 2)));
                    points.Add(new Point(element.ActualWidth, element.ActualHeight));
                    points.Add(new Point(0, element.ActualHeight));
                }
            };

            fillPoints();
            element.SizeChanged += (s, e) => fillPoints();
            horizonModel.PropertyChanged += (s, e) => fillPoints();

            return points;
        }

        private static Action EmptyDelegate = delegate () { };



        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


    }
    public enum DisplayTypes
    {
        Sky,
        Ground
    }
}

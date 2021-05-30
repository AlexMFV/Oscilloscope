using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Oscilloscope
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public float _period = 6.0f;
        public float _amplitude = 10.0f;
        public float _freq = 3.0f;

        public double _segment;

        public MainWindow()
        {
            InitializeComponent();
            _segment = canvas.ActualWidth / _period;
        }

        public void DrawLine()
        {
            ClearScreen();
            Polyline poly = new Polyline();
            List<Point> points = new List<Point>();

            for (double x = 0.0f; x <= _period; x += _period/500)
            {
                double yValue = Math.Sin(x * 2 * Math.PI); // Since you want 2*PI to be at 1
                double y = ConvertToRange(-2.5f, 2.5f, yValue);
                points.Add(new Point((x*(100/_period)) * (canvas.ActualWidth / 100), y));
            }

            //for (int i = 0; i < canvas.ActualWidth; i++)
            //{
            //    double y = _amplitude * Math.Sin(2 * Math.PI * _freq * i);

            //    points.Add(new Point(i* (canvas.ActualWidth / 18), canvas.ActualHeight/2 + y));
            //}

            poly.Points = new PointCollection(points);
            poly.StrokeThickness = 3;
            poly.Stroke = new SolidColorBrush(Colors.LimeGreen);
            canvas.Children.Add(poly);
        }

        private void canvas_Loaded(object sender, RoutedEventArgs e)
        {
            DrawLine();
        }

        public double ConvertToRange(double min, double max, double value)
        {
            double NewValue = (((value - min) * (canvas.ActualHeight - 0)) / (max - min)) + 0;
            return NewValue;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DrawLine();
        }

        private void slideAmplitude_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void slideCycles_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _period = (float)slideCycles.Value;
            DrawLine();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawLine();
        }

        public void ClearScreen()
        {
            canvas.Children.Clear();
        }
    }
}
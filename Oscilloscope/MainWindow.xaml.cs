using NAudio.Wave;
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
using System.Windows.Threading;

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

        public float _offset = 0.0f;

        public double _segment;

        DateTime _lastTime; // marks the beginning the measurement began
        int _framesRendered; // an increasing count
        int _fps; // the FPS calculated from the last measurement

        public MainWindow()
        {
            InitializeComponent();
            _segment = canvas.ActualWidth / _period;

            DispatcherTimer loopTick = new DispatcherTimer();
            loopTick.Tick += LoopTick_Tick; ;
            loopTick.Interval = TimeSpan.FromMilliseconds(8.3f); //60FPS
            loopTick.Start();
        }

        private void LoopTick_Tick(object sender, EventArgs e)
        {
            Oscillate();
        }

        public void DrawLine(float offset = 0.01f)
        {
            ClearScreen();
            Polyline poly = new Polyline();
            List<Point> points = new List<Point>();
            List<double> values = new List<double>();

            if (_offset > 2*Math.PI)
                _offset = 0.01f;
            else
                _offset += offset;

            for (double x = 0.0f; x <= _period; x += _period/250)
            {
                double yValue = Math.Sin(x * 2 * Math.PI + _offset); // Since you want 2*PI to be at 1
                double y = ConvertToRange(-2.5f, 2.5f, yValue);
                values.Add(y);
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

            //WaveOut waveOut = new WaveOut();
            //waveOut.Init(new Oscillator(values.ToArray()));
            //while(true)
                //waveOut.Play();

            canvas.Children.Add(poly);
        }

        private void canvas_Loaded(object sender, RoutedEventArgs e)
        {
            //DrawLine();
        }

        public double ConvertToRange(double min, double max, double value)
        {
            double NewValue = (((value - min) * (canvas.ActualHeight - 0)) / (max - min)) + 0;
            return NewValue;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //DrawLine();
        }

        private void slideAmplitude_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _amplitude = (float)slideAmplitude.Value;
        }

        private void slideCycles_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _period = (float)slideCycles.Value;
            //DrawLine();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //DrawLine();
        }

        public void ClearScreen()
        {
            canvas.Children.Clear();
        }

        public void Oscillate()
        {
            DrawLine(_amplitude);
            Update() ;
        }

        void Update()
        {
            _framesRendered++;

            if ((DateTime.Now - _lastTime).TotalSeconds >= 1)
            {
                // one second has elapsed 

                _fps = _framesRendered;
                _framesRendered = 0;
                _lastTime = DateTime.Now;
            }

            // draw FPS on screen here using current value of _fps
            lblFPS.Text = _fps + " FPS";
        }
    }
}
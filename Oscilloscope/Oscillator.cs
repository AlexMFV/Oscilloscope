using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oscilloscope
{
    class Oscillator : WaveProvider16
    {
        double[] values;

        public Oscillator(double[] _values)
        {
            this.values = _values;
        }

        public double Frequency { set; get; }
        public short Amplitude { set; get; }

        public override int Read(short[] buffer, int offset,
          int sampleCount)
        {
            return (int)this.values[offset];
        }
    }
}

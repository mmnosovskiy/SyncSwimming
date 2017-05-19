using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncSwimming
{
    public class Scores
    {
        private double[] _judjes = new double[7];
        public string Name { get; set; }
        public double[] Refferies
        {
            get
            {
                return _judjes;
            }
            set
            {
                _judjes = value;
            }
        }
        public double Coef { get; set; }
        public double Result
        {
            get
            {
                return Sum5() * Coef;
            }
        }

        public double Sum5()
        {
            double sum = 0, min = 11, max = 0;
            for (int i = 0; i < Refferies.Length; i++)
            {
                if (Refferies[i] > max) max = Refferies[i];
                if (Refferies[i] < min) min = Refferies[i];
                sum += Refferies[i];
            }
            return sum - max - min;
        }
    }
}

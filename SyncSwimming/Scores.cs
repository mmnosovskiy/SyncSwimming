using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncSwimming
{
    public class Scores
    {
        private double[] _judjesOP = new double[7];
        private double[] _judjesPP = new double[5];
        public string Name { get; set; }
        public double[] RefferiesOP
        {
            get
            {
                return _judjesOP;
            }
            set
            {
                _judjesOP = value;
            }
        }
        public double[] RefferiesPP
        {
            get
            {
                return _judjesPP;
            }
            set
            {
                _judjesPP = value;
            }
        }
        public double Coef { get; set; }
        public double ResultOP
        {
            get
            {
                return Sum5() * Coef;
            }
        }
        public double ResultPP
        {
            get
            {
                return Sum3() * Coef;
            }
        }

        public double Sum5()
        {
            double sum = 0, min = 11, max = 0;
            for (int i = 0; i < RefferiesOP.Length; i++)
            {
                if (RefferiesOP[i] > max) max = RefferiesOP[i];
                if (RefferiesOP[i] < min) min = RefferiesOP[i];
                sum += RefferiesOP[i];
            }
            return sum - max - min;
        }
        public double Sum3()
        {
            double sum = 0, min = 11, max = 0;
            for (int i = 0; i < RefferiesPP.Length; i++)
            {
                if (RefferiesPP[i] > max) max = RefferiesPP[i];
                if (RefferiesPP[i] < min) min = RefferiesPP[i];
                sum += RefferiesPP[i];
            }
            return sum - max - min;
        }
    }
}

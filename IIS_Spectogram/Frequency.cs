using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIS_Spectogram
{
    internal class Frequency
    {
        private double val;

        public double Val { get { return val; }  set { val = value; } }

        public Frequency(double val) {
            this.val = val;

        }
    }
}

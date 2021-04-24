using System;
using System.Collections.Generic;
using System.Text;

namespace GA_System
{
    public class ExactSolution
    {
        public double GetValue(double x)
        {
            return Math.Sin(x) + Math.Cos(x) * 2;
        }
    }
}

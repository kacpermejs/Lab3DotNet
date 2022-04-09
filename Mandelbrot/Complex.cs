using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandelbrot
{
    public class Complex
    {
        public double a;
        public double b;

        public Complex(double a, double b)
        {
            this.a = a;
            this.b = b;
        }
        public void Square()
        {
            double temp = (a*a) - (b*b);
            b = 2.0 * a * b;
            a = temp;
        }
        public double Mod()
        {
            return Math.Sqrt((a * a) + (b * b));
        }
        public double ModSquared()
        {
            return ((a * a) + (b * b));
        }

        
        public static Complex operator +(Complex z1, Complex z2) => new Complex(z1.a + z2.a, z1.b + z2.b);

        
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Astronomy
{
    public class Planet
    {
        public string Name { get; internal set; }

        public double M0 { get; internal set; }

        public double M1 { get; internal set; }

        public double Theta0 { get; internal set; }

        public double Theta1 { get; internal set; }

        public double C1 { get; internal set; }

        public double C2 { get; internal set; }

        public double C3 { get; internal set; }

        public double C4 { get; internal set; }

        public double C5 { get; internal set; }

        public double C6 { get; internal set; }

        public double P { get; internal set; }

        public double Eps { get; internal set; }

        public Planet(string name, double m0, double m1, double theta0, double theta1,
            double c1, double c2, double c3, double c4, double c5, double c6, double p, double eps)
        {
            Name = name;
            M0 = m0;
            M1 = m1;
            Theta0 = theta0;
            Theta1 = theta1;
            C1 = c1;
            C2 = c2;
            C3 = c3;
            C4 = c4;
            C5 = c5;
            C6 = c6;
            P = p;
            Eps = eps;
        }
    }
}

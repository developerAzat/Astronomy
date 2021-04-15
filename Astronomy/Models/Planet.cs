using System;
using System.Collections.Generic;
using System.Text;

namespace Astronomy
{
    /// <summary>
    /// The class contains the main astronomical properties of the planet.
    /// </summary>
    public class Planet
    {
        /// <summary>
        /// Solar System Planet.
        /// </summary>
        public PlanetName Name { get; internal set; }

        /// <summary>
        /// Mean anomaly at 01.01.2020.
        /// </summary>
        public double M0 { get; internal set; }

        /// <summary>
        /// Mean angular motion.
        /// </summary>
        public double M1 { get; internal set; }

        /// <summary>
        /// The sidereal time in degrees at longitude 0°.
        /// </summary>
        public double Theta0 { get; internal set; }

        /// <summary>
        /// The planet's rotation rate.
        /// </summary>
        public double Theta1 { get; internal set; }

        /// <summary>
        /// First coefficient of Kepler's equation.
        /// </summary>
        public double C1 { get; internal set; }

        /// <summary>
        /// Second coefficient of Kepler's equation.
        /// </summary>
        public double C2 { get; internal set; }

        /// <summary>
        /// Third coefficient of Kepler's equation.
        /// </summary>
        public double C3 { get; internal set; }

        /// <summary>
        /// Fourth coefficient of Kepler's equation.
        /// </summary>
        public double C4 { get; internal set; }

        /// <summary>
        /// Fifth coefficient of Kepler's equation.
        /// </summary>
        public double C5 { get; internal set; }

        /// <summary>
        /// Sixth coefficient of Kepler's equation.
        /// </summary>
        public double C6 { get; internal set; }

        /// <summary>
        /// The perihelion of the planet.
        /// </summary>
        public double P { get; internal set; }

        /// <summary>
        /// The obliquity of the planet's equator.
        /// </summary>
        public double Eps { get; internal set; }

        /// <summary>
        /// Creates an instance of the planet.
        /// </summary>
        public Planet(PlanetName name, double m0, double m1, double theta0, double theta1,
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

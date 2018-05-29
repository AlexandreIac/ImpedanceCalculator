using System;
using System.Numerics;

namespace ImpedanceCalculator.src
{
    class Capacitor : Element
    {

        public double Value
        {
            get { return this.value; }
            set
            {
                this.value = value;
                ValueChanged(this, value);
            }
        }

        public Capacitor(string Name, int Id, double value)
            : base(Name, Id, value, "capacitor")
        {
        }

        public event FncGenericEvent ValueChanged;

        override public Complex CalculateZ(double frequency)
        {
            Complex Xl = new Complex(0, 1 / (2 * Math.PI * frequency * value));
            return (Xl);
        }
    }
}

using System;
using System.Numerics;

namespace ImpedanceCalculator.src
{
    class Resistor : Element
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

        public Resistor(string Name, int Id, double value)
            : base(Name, Id, value, "resistor")
        {
        }

        public event FncGenericEvent ValueChanged;

        override public Complex CalculateZ(double frequency)
        {
            Complex R = new Complex(value, 0);
            return (R);
        }
    }
}

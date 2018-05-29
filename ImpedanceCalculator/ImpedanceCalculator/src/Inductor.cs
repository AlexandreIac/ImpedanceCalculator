using System;
using System.Numerics;

namespace ImpedanceCalculator.src
{
    class Inductor : Element
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

        public Inductor(string Name, int Id, double value)
            : base(Name, Id, value, "inductor")
        {
        }

        public event FncGenericEvent ValueChanged;

        override public Complex CalculateZ(double frequency)
        {
            Complex Xc = new Complex(0, 2 * Math.PI * frequency * value);
            return (Xc);
        }
    }
}

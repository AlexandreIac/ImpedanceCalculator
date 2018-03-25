using System;
using System.Numerics;

namespace ImpedanceCalculator.src
{
    class Resistor : IElement
    {
        public Resistor(double value, string name, int id)
        {
            this.Name = name;
            this.value = value;
            this.id = id;
            this.element = "resistor";
        }

        public int id { get; set; }

        public string element { get; set; }
        public string Name { get; set; }
        private double value { get; set; }
        public double Value
        {
            get { return this.value; }
            set
            {
                this.value = value;
                ValueChanged(this, value);
            }
        }

        public event FncGenericEvent ValueChanged;

        public Complex CalculateZ(double frequency)
        {
            Complex R = new Complex(value, 0);
            return (R);
        }
    }
}

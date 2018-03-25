using System;
using System.Numerics;

namespace ImpedanceCalculator.src
{
    class Inductor : IElement
    {
        public Inductor(double value, string name, int id)
        {
            this.Name = name;
            this.value = value;
            this.id = id;
            this.element = "inductor";
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
                ValueChanged(this, null);
            }
        }

        public event FncGenericEvent ValueChanged;

        public Complex CalculateZ(double frequency)
        {
            Complex Xc = new Complex(0, 2 * Math.PI * frequency * value);
            return (Xc);
        }
    }
}

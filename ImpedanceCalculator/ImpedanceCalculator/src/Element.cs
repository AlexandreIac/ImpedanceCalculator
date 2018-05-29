using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ImpedanceCalculator
{
    public delegate void FncGenericEvent(object obj1, object obj2);
    public abstract class Element
    {
        public int Id { get; }
        public string Name { get; set; }
        public string element { get; }
        protected double value;

        protected Element(string Name, int Id, double Value, string _element)
        {
            this.Id = Id;
            this.Name = Name;
            this.value = Value;
            this.element = _element;
        }

        public abstract Complex CalculateZ(double frequency);

        event FncGenericEvent ValueChanged;
    }
}

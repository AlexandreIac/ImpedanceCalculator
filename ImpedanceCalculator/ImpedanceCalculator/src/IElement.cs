using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ImpedanceCalculator
{
    public delegate void FncGenericEvent(object obj1, object obj2);
    interface IElement
    {
        int id { get; set; }
        string Name { get; set; }
        double Value { get; set; }
        Complex CalculateZ(double frequency);

        event FncGenericEvent ValueChanged;
    }
}

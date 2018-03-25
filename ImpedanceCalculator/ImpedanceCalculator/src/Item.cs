using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpedanceCalculator.src
{
    class Item
    {
            public double Value { get; set; }
            public string Element { get; set; }
            public string Name { get; set; }
        public int Id { get; set; }
    }

    class Result
    {
        public double Real { get; set; }
        public double Imaginary { get; set; }
        public double Impedance { get; set; }
    }
}

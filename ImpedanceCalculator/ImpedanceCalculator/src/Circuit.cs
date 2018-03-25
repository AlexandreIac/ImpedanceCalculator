using ImpedanceCalculator.src;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace ImpedanceCalculator
{
    class Circuit
    {
        List<IElement> elements = null;

        public event FncGenericEvent CircuitChanged;

        public Complex[] CalculateZ(double[] frequency)
        {
            Complex[] listComplex = new Complex[frequency.Length];
            Complex tmp;
            Type[] expected = new Type[3] { typeof(Resistor), typeof(Inductor) , typeof(Capacitor) };
            List<double>[] sum = new List<double>[3];
            sum[0] = new List<double>();
            sum[1] = new List<double>();
            sum[2] = new List<double>();
            double Xc = 0;
            double Xl = 0;
            int index = 0;
            foreach (double d in frequency)
            {
                sum[0].Clear();
                sum[1].Clear();
                sum[2].Clear();
                for (int i = 0; i < 3; i++)
                {
                    foreach (IElement e in elements)
                    {
                        if (e.GetType() == expected[i])
                        {
                            tmp = e.CalculateZ(frequency[index]);
                            if (i == 0)
                                sum[i].Add(tmp.Real);
                            else
                                sum[i].Add(tmp.Imaginary);
                        }
                    }
                }
                Xl = GetSum(sum[1]);
                Xc = GetSum(sum[2]);
                if (Xc < Xl)
                    listComplex[index] = new Complex(GetSum(sum[0]), Xl - Xc);
                else
                    listComplex[index] = new Complex(GetSum(sum[0]), Xc - Xl);
                index += 1;
            }
            return (listComplex);
        }

        private double GetSum(List<double> list)
        {
            double sum = 0;
            foreach (double d in list)
            {
                sum += d;
            }
            return (sum);
        }

        public void CreateCircuit(List<IElement> newElements)
        {
            if (elements != null)
            {
                foreach (IElement elem in elements)
                    elem.ValueChanged -= this.HandleElementValueChanged;
            }
            elements = newElements;
            foreach (IElement elem in elements)
                elem.ValueChanged += this.HandleElementValueChanged;
        }

        private void HandleElementValueChanged(object obj1, object obj2)
        {
            CircuitChanged(obj1, obj2);
        }

        public IElement GetElement(int id)
        {
            foreach (IElement e in elements)
            {
                if (e.id == id)
                    return (e);
            }
            return (null);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using ImpedanceCalculator.src;

namespace ImpedanceCalculator
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Circuit circuit = new Circuit();
        private Item selected;

        public MainWindow()
        {
            InitializeComponent();
            circuit.CircuitChanged += this.OnCircuitChangedEvent;
        }

        private void OnCircuitChangedEvent(object obj1, object obj2)
        {
            if (frequencyList.Items.IsEmpty)
            {
                errorFrequency.Text = "enter a frequency";
                return;
            }
                
            double[] listDouble = new double[frequencyList.Items.Count];
            int index = 0;
            foreach (double d in frequencyList.Items)
            {
                listDouble[index] = d;
                index += 1;
            }
            Complex[] list = circuit.CalculateZ(listDouble);
            Result.Items.Clear();
            foreach (Complex c in list)
            {
                this.Result.Items.Add(new Result { Real = c.Real, Imaginary = c.Imaginary, Impedance = Math.Sqrt(Math.Pow(c.Real, 2) + Math.Pow(c.Imaginary, 2)) });
            }
        }

        private void AddButton(object sender, RoutedEventArgs e)
        {
            string newText = NewFrequency.Text;
            bool isNumeric = Double.TryParse(newText, out double n);
            if (!isNumeric)
            {
                errorFrequency.Text = "enter double";
                return;
            }
            if (newText.Length > 7)
            {
                errorFrequency.Text = "number too long";
                return;
            }
            try
            {
                double value = Convert.ToDouble(newText);
                frequencyList.Items.Add(value);
            }
            catch (FormatException)
            {
                errorFrequency.Text = "wrong format (use ',' to enter a double number)";
                return;
            }
            catch (OverflowException)
            {
                errorFrequency.Text = "number too long or too short";
                return;
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void NewFrequency_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void ButtonRL(object sender, RoutedEventArgs e)
        {
            selected = null;
            ElementsList.Items.Clear();
            this.ElementsList.Items.Add(new Item { Element = "resistor", Value = 1.0, Name = "Res" , Id = 1});
            this.ElementsList.Items.Add(new Item { Element = "inductor", Value = 1.0, Name = "Ind", Id = 2 });
            circuit.CreateCircuit(new List<IElement>() { new Resistor(1.0, "Res", 1), new Inductor(1.0, "Ind", 2)});
        }

        private void ButtonRC(object sender, RoutedEventArgs e)
        {
            selected = null;
            ElementsList.Items.Clear();
            this.ElementsList.Items.Add(new Item { Element = "resistor", Value = 1.0, Name = "Res", Id = 1 });
            this.ElementsList.Items.Add(new Item { Element = "capacitor", Value = 1.0, Name = "Cap", Id = 2 });
            circuit.CreateCircuit(new List<IElement>() { new Resistor(1.0, "Res", 1), new Capacitor(1.0, "Cap", 2) });
        }

        private void ButtonRLC(object sender, RoutedEventArgs e)
        {
            selected = null;
            ElementsList.Items.Clear();
            this.ElementsList.Items.Add(new Item { Element = "resistor", Value = 1.0, Name = "Res", Id = 1 });
            this.ElementsList.Items.Add(new Item { Element = "inductor", Value = 1.0, Name = "Ind", Id = 2 });
            this.ElementsList.Items.Add(new Item { Element = "capacitor", Value = 1.0, Name = "Cap", Id = 3 });
            circuit.CreateCircuit(new List<IElement>() { new Resistor(1.0, "Res", 1), new Inductor(1.0, "Ind", 2), new Capacitor(1.0, "Cap", 3)});
        }

        private void ButtonLC(object sender, RoutedEventArgs e)
        {
            selected = null;
            ElementsList.Items.Clear();
            this.ElementsList.Items.Add(new Item { Element = "Capacitor", Value = 1.0, Name = "Cap", Id = 1 });
            this.ElementsList.Items.Add(new Item { Element = "inductor", Value = 1.0, Name = "Ind", Id = 2 });
            circuit.CreateCircuit(new List<IElement>() { new Capacitor(1.0, "Cap", 1), new Inductor(1.0, "Ind", 2) });
        }

        private void ListView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                Item selectedRow = e.AddedItems[0] as Item;
                selected = e.AddedItems[0] as Item;
                if (selectedRow != null)
                {
                    EnterName.Text = selectedRow.Name;
                    EnterValue.Text = selectedRow.Value.ToString();
                }
            }
        }

        private void EnterName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (selected != null)
            {
                this.ElementsList.Items[selected.Id - 1] = new Item { Element = selected.Element, Value = selected.Value, Name = EnterName.Text, Id = selected.Id };
                selected.Name = EnterName.Text;
            }
        }

        private void EnterValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (selected != null)
            {
                try
                {
                    double value = Convert.ToDouble(EnterValue.Text);
                    this.ElementsList.Items[selected.Id - 1] = new Item { Element = selected.Element, Value = value, Name = selected.Name, Id = selected.Id };
                    selected.Value = value;
                }
                catch (FormatException)
                {
                    ErrorValue.Text = "wrong format (use ',' to enter a double number)";
                    return;
                }
                catch (OverflowException)
                {
                    ErrorValue.Text = "number too long or too short";
                    return;
                }
                IElement elem;
                if ((elem = circuit.GetElement(selected.Id)) == null)
                    return;
                elem.Value = selected.Value;
            }
        }
    }
}

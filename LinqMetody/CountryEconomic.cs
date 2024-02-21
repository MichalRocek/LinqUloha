using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqMetody
{
    public class CountryEconomic
    {
        public string Name { get; set; }
        public double GDP { get; set; }
        public double Population { get; set; }
        public double PerCapita { get; set; }
        public override string ToString()
        {
            return String.Format("{0}, {1} people, {2} GDP, ({3} per capita)", Name, Population, GDP, PerCapita);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqMetody
{
    public class Country
    {
        public string Name { get; set; }
        public double? Population { get; set; }
        public double LandArea { get; set; }
        public Continent Continent { get; set; }
        public double? FertilityRate { get; set; }
        public double? GDP { get; set; } // in mil of USD
        public override string ToString()
        {
            return String.Format("{0} on {1}, {2} people, {3} km2, {4}, {5}", Name, Continent, Population, LandArea, GDP, FertilityRate);
        }
    }
    public enum Continent
    {
        Africa,
        Asia,
        Europe,
        NorthAmerica,
        SouthAmerica,
        AustraliaOceania,
        Antarctica
    }
}

using System.Diagnostics.Metrics;
using LinqMetody;

internal class Program
{
    private static void Main(string[] args)
    {
        //data
        List<Country> countries = new List<Country>
{
     new Country{ Name = "China", Continent = Continent.Asia, Population = 1406357120, LandArea = 9388211, FertilityRate = 1.7, GDP = 14860775 },
     new Country{ Name = "India", Continent = Continent.Asia, Population = 1380004385, LandArea = 2973190, FertilityRate = 2.2, GDP = 2592583 },
     new Country{ Name = "United States", Continent = Continent.NorthAmerica, Population = 331002651, LandArea = 9147420, FertilityRate = 1.8, GDP = 20807269 },
     new Country{ Name = "Indonesia", Continent = Continent.Asia, Population = 273523615, LandArea = 1811570, FertilityRate = 2.3, GDP = 1088768 },
     new Country{ Name = "Nigeria", Continent = Continent.Africa, Population = 206139589, LandArea = 910770, FertilityRate = 5.4, GDP = 442976 },
     new Country{ Name = "Russia", Continent = Continent.Europe, Population = 145934462, LandArea = 16376870, FertilityRate = 1.8, GDP = 1464078 },
     new Country{ Name = "Czechia", Continent = Continent.Europe, Population = 10708981, LandArea = 77240, FertilityRate = 1.6, GDP = 241975 },
     new Country{ Name = "Poland", Continent = Continent.Europe, Population = 37846611, LandArea = 306230, FertilityRate = 1.4, GDP = 580894 },
     new Country{ Name = "Slovakia", Continent = Continent.Europe, Population = 5459642, LandArea = 48088, FertilityRate = 1.5, GDP = 101892 },
     new Country{ Name = "France", Continent = Continent.Europe, Population = 65273511, LandArea = 547557, FertilityRate = 1.9, GDP = 2551451 },
     new Country{ Name = "United Kingdom", Continent = Continent.Europe, Population = 67886011, LandArea = 241930, FertilityRate = 1.8, GDP = 2638296 },
     new Country{ Name = "Germany", Continent = Continent.Europe, Population = 83783942, LandArea = 547557, FertilityRate = 1.8, GDP = 3780553 },
     new Country{ Name = "Egypt", Continent = Continent.Africa, Population = 102334404, LandArea = 995450, FertilityRate = 3.3, GDP = 361875 },
     new Country{ Name = "Venezuela", Continent = Continent.SouthAmerica, Population = 28435940, LandArea = 882050, FertilityRate = null, GDP = 134960 },
     new Country{ Name = "Monaco", Continent = Continent.Europe, Population = 39242, LandArea = 1, FertilityRate = null, GDP = 7423 }
};

        //Použijte jen funkce LINQ (Select, Where, OrderBy, OrderByDescending, Take, Count, Group)
        //1) Seřaďte data podle názvu a vypište je
        countries.OrderBy(country => country.Name).Print("1) Seřaďte data podle názvu a vypište je:", "");
        //2) Seřaďte data podle hustoty osídlení a vypište je
        countries.OrderByDescending(country => country.Population / country.LandArea).Print("2) Seřaďte data podle hustoty osídlení a vypište je:", "");
        //3) Získejte jen data zemí z Evropy a vypište je
        countries.Where(country => country.Continent == Continent.Europe).Print("3) Získejte jen data zemí z Evropy a vypište je:", "");
        //4) Získejte data zemí, které mají porodnost větší než 2 a seřaďte je sestupně podle porodnosti
        countries.Where(country => country.FertilityRate > 2).OrderByDescending(country => country.FertilityRate).Print("4) Získejte data zemí, které mají porodnost větší než 2 a seřaďte je sestupně podle porodnosti:", "");
        //5) Získejte data všech zemí, ale spočítejte jejich HDP na hlavu a data vypište přes Select jako CountryEconomic
        countries.Select(country => new CountryEconomic { GDP = (double)country.GDP!, Name = country.Name, Population = (double)country.Population!, PerCapita = (double)(country.GDP / country.Population)! }).Print("5) Získejte data všech zemí, ale spočítejte jejich HDP na hlavu a data vypište přes Select jako CountryEconomic:", "");
        //6) Vypište, kolik zemí máme na jakém kontinentu (Group)
        countries.GroupBy(country => country.Continent).Select(continent => continent.Key + ": " + continent.Count()).Print("6) Vypište, kolik zemí máme na jakém kontinentu (Group):", "");
        //7) Vypište, kolik obyvatel z námi vybraných zemí žije na kterém kontinentu
        countries.GroupBy(country => country.Continent).Select(continent => continent.Key + ": " + continent.Sum(country => country.Population)).Print("7) Vypište, kolik obyvatel z námi vybraných zemí žije na kterém kontinentu:", "");
        //8) Vypište 5 zemí s největší populací
        countries.OrderByDescending(country => country.Population).Take(5).Print("8) Vypište 5 zemí s největší populací:", "");
        //9) Vypište 5 zemí s největší hustotou obyvatelstva
        countries.OrderByDescending(country => country.Population / country.LandArea).Take(5).Print("9) Vypište 5 zemí s největší hustotou obyvatelstva:", "");
        //10) Vypište země, jejichž název začíná písmenem "C" (String.StartsWith)
        countries.Where(country => country.Name.StartsWith('C')).Print("10) Vypište země, jejichž název začíná písmenem \"C\" (String.StartsWith):", "");

        //Vlastní rozšiřující metody:
        //1) Metoda countries.Random(x)
        countries.Random(3).Print("1) Metoda countries.Random(x):");
        try
        {
            countries.Random(100); //mělo by dojít k výjimce
        }
        catch (Exception ex)
        {
            ex.Print("Výjimka 1:");
        }
        try
        {
            countries.Random(-1); //mělo by dojít k výjimce
        }
        catch (Exception ex)
        {
            ex.Print("Výjimka 2:", "");
        }
        //2) Metoda countries.InEurope()
        countries.InEurope().Print("2) Metoda countries.InEurope():", "");
        //5) Metoda countries.PopulationDensity()
        countries.PopulationDensity().Print("Metoda countries.PopulationDensity():", "");
    }
}


public static class ExtensionMethods
{
    //Vytvořte vlastní rozšiřující metody:
    //1) Vytvořte vlastní extension metodu, která vrátí několik náhodných zemi z celého seznamu (countries.Random(x))
    private static Random _random = new Random();
    public static List<Country> Random(this List<Country> list, int count)
    {
        if (count > list.Count()) throw new ArgumentOutOfRangeException(String.Format("There aren't that many ({0}) elements in the list of length {1}", count, list.Count()));
        if (count < 0) throw new ArgumentException("Can't ask for negative amount of elements");
        return list.OrderBy(element => _random.Next(list.Count())).Take(count).ToList();
    }
    //2) Vytvořte vlastní extension metodu, která ze seznamu zemí vrátí jen evropské země (countries.InEurope())
    public static List<Country> InEurope(this List<Country> countries) => countries.Where(country => country.Continent == Continent.Europe).ToList();
    //3) Vytvořte definici delegátu, který dostane dvě čísla a vrátí jiné číslo
    public delegate double EvalDoubles(double a, double b);
    //4) Použijte tento delegát k vytvoření funkce, která spočítá hustotu obyvatel na základě předané populace
    private static EvalDoubles _populationDensity = (population, area) => population / area;
    //5) Vytvořte extension metodu, která pomocí této nové funkce vrátí hustotu obyvatel v každé zemi
    public static List<double> PopulationDensity(this List<Country> countries) => countries.Select(country => _populationDensity((double)country.Population!, country.LandArea)).ToList();
}
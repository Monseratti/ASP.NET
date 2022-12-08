namespace CW_0112.Models
{
    public class Car
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string Manufacturer { get; set; }
        public int Year { get; set; }
        public int EngineV { get; set; }

        public Car(string name, string color, string manufacturer, int year, int engineV)
        {
            Name = name;
            Color = color;
            Manufacturer = manufacturer;
            Year = year;
            EngineV = engineV;
        }

        public override string ToString()
        {
            return $"{Name},{Manufacturer},{Color} - {Year} - {EngineV} cm3";
        }
    }
}

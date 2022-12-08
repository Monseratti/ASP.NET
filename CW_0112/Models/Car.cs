namespace CW_0112.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Manufacturer { get; set; }
        public int Year { get; set; }
        public int EngineV { get; set; }

        public Car()
        {
            Name = Color = Manufacturer = "";
            Id=Year = EngineV = 0;
        }

        public Car(int id, string name, string color, string manufacturer, int year, int engineV)
        {
            Id = id;
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

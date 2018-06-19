using System;
using MongoDB.Bson;

namespace Sensor.Node.Modules.Digital
{
    class TemperatureModule : Module
    {
        public override int Type
        {
            get { return 0x02; }
        }

        public float Temperature { get; private set; }
        public float Humidity { get; private set; }

        public TemperatureModule(Station station) : base(station, 0)
        {

        }

        public override void Decode(BsonDocument document)
        {
            this.Temperature = (float)document["temp"].AsDouble;
            this.Humidity = (float)document["hd"].AsDouble;
        }

        public override void Tick()
        {
            Console.WriteLine(this.Temperature.ToString("Temperature: 00.00 °C"));
            Console.WriteLine(this.Humidity.ToString("Humidity: 00.00 \\%"));
        }
    }
}

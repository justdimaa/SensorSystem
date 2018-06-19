using System;
using MongoDB.Bson;

namespace Sensor.Node.Modules.Digital
{
    class OpticalDensityModule : Module
    {
        public override int Type
        {
            get { return 0x01; }
        }

        public int Lux { get; private set; }

        public OpticalDensityModule(Station station) : base(station, 0)
        {

        }

        public override void Decode(BsonDocument document)
        {
            this.Lux = (int)document["lux"].AsDouble;
        }

        public override void Tick()
        {
            Console.WriteLine(this.Lux.ToString("Lux: 0 lx"));
        }
    }
}

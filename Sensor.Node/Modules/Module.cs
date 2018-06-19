using MongoDB.Bson;

namespace Sensor.Node.Modules
{
    abstract class Module
    {
        public abstract int Type { get; }

        public Station Station { get; }
        public int Resolution { get; }

        protected Module(Station station, int resolution)
        {
            this.Station = station;
            this.Resolution = resolution;
        }

        public abstract void Decode(BsonDocument document);

        public abstract void Tick();
    }
}

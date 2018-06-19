namespace Sensor.Node.Messages
{
    abstract class Message
    {
        public Station Station { get; }

        protected Message(Station station)
        {
            this.Station = station;
        }
    }
}

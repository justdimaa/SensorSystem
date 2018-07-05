namespace Sensor.Node.Messages
{
    abstract class Message
    {
        public Station Station { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="station"></param>
        protected Message(Station station)
        {
            this.Station = station;
        }
    }
}

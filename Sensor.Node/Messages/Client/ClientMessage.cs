using MongoDB.Bson;

namespace Sensor.Node.Messages.Client
{
    abstract class ClientMessage : Message
    {
        public abstract State RequiredState { get; }

        protected ClientMessage(Station station) : base(station)
        {

        }

        public abstract void Decode(BsonDocument document);

        public abstract void Execute();
    }
}

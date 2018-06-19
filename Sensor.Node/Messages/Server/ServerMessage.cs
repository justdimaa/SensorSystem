using MongoDB.Bson;

namespace Sensor.Node.Messages.Server
{
    abstract class ServerMessage : Message
    {
        public abstract int Identifier { get; }
        public BsonDocument Document { get; }

        protected ServerMessage(Station station) : base(station)
        {
            this.Document = new BsonDocument();
        }

        public virtual void Encode()
        {
            this.Document["msg_id"] = this.Identifier;
        }
    }
}

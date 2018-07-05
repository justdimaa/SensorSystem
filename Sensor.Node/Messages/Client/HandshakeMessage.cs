using MongoDB.Bson;
using Sensor.Node.Messages.Server;

namespace Sensor.Node.Messages.Client
{
    internal sealed class HandshakeMessage : ClientMessage
    {
        internal override State RequiredState
        {
            get { return State.Handshake; }
        }

        private string Name { get; set; }

        public HandshakeMessage(Station station) : base(station)
        {

        }

        internal override void Decode(BsonDocument document)
        {
            this.Name = document["name"].AsString;
        }

        internal override void Execute()
        {
            this.Station.Name = this.Name;
            this.Station.State = State.Active;

            this.Station.SendData(new HandshakeOkMessage(this.Station));
        }
    }
}

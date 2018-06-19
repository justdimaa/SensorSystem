namespace Sensor.Node.Messages.Server
{
    class HandshakeOkMessage : ServerMessage
    {
        public override int Identifier
        {
            get { return 0xC8; }
        }

        public HandshakeOkMessage(Station station) : base(station)
        {

        }

        public override void Encode()
        {
            base.Encode();

            this.Document["name"] = this.Station.Name;
        }
    }
}

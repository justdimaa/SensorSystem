using MongoDB.Bson;

namespace Sensor.Node.Messages.Client
{
    internal sealed class RemoveModuleMessage : ClientMessage
    {
        internal override State RequiredState
        {
            get { return State.Active; }
        }

        private int ModuleID { get; set; }

        internal RemoveModuleMessage(Station station) : base(station)
        {

        }

        internal override void Decode(BsonDocument document)
        {
            this.ModuleID = document["module_id"].AsInt32;
        }

        internal override void Execute()
        {
            //this.Station.ModuleManager.Remove...();
        }
    }
}

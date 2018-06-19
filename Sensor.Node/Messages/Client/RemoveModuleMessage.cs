using MongoDB.Bson;
using Sensor.Node.Modules;

namespace Sensor.Node.Messages.Client
{
    class RemoveModuleMessage : ClientMessage
    {
        public override State RequiredState
        {
            get { return State.Active; }
        }

        private int ModuleID { get; set; }

        public RemoveModuleMessage(Station station) : base(station)
        {

        }

        public override void Decode(BsonDocument document)
        {
            this.ModuleID = document["module_id"].AsInt32;
        }

        public override void Execute()
        {
            //this.Station.Modules.Remove();
        }
    }
}

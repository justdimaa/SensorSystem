using MongoDB.Bson;
using Sensor.Node.Modules;

namespace Sensor.Node.Messages.Client
{
    class UpdateModuleInfoMessage : ClientMessage
    {
        public override State RequiredState
        {
            get { return State.Active; }
        }

        private int ModuleID { get; set; }

        private Module Module { get; set; }

        public UpdateModuleInfoMessage(Station station) : base(station)
        {

        }

        public override void Decode(BsonDocument document)
        {
            this.ModuleID = document["module_id"].AsInt32;
            this.Module = this.Station.Modules.Find(m => m.Type == this.ModuleID);
            this.Module?.Decode(document);
        }

        public override void Execute()
        {
            this.Module?.Tick();
        }
    }
}

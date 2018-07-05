using MongoDB.Bson;
using Sensor.Node.Modules;

namespace Sensor.Node.Messages.Client
{
    internal sealed class UpdateModuleInfoMessage : ClientMessage
    {
        internal override State RequiredState
        {
            get { return State.Active; }
        }

        private int ModuleID { get; set; }

        private Module Module { get; set; }

        public UpdateModuleInfoMessage(Station station) : base(station)
        {

        }

        internal override void Decode(BsonDocument document)
        {
            this.ModuleID = document["module_id"].AsInt32;
            this.Module = this.Station.ModuleManager.GetModule(this.ModuleID);
            this.Module?.Decode(document);
        }

        internal override void Execute()
        {
            this.Module?.Tick();
        }
    }
}

using MongoDB.Bson;
using Sensor.Node.Modules;
using Sensor.Node.Modules.Digital;

namespace Sensor.Node.Messages.Client
{
    class AddModuleMessage : ClientMessage
    {
        public override State RequiredState
        {
            get { return State.Active; }
        }

        private int ModuleID { get; set; }
        private int Resolution { get; set; }

        public AddModuleMessage(Station station) : base(station)
        {

        }

        public override void Decode(BsonDocument document)
        {
            this.ModuleID = document["module_id"].AsInt32;
            //this.Resolution = document["res"].AsInt32;
        }

        public override void Execute()
        {
            Module module = null;

            switch (this.ModuleID)
            {
                case 0x01:
                    module = new OpticalDensityModule(this.Station);
                    break;
                case 0x02:
                    module = new TemperatureModule(this.Station);
                    break;
            }

            if (module != null)
                this.Station.Modules.Add(module);
        }
    }
}

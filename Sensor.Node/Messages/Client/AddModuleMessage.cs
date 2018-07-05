using MongoDB.Bson;

namespace Sensor.Node.Messages.Client
{
    internal sealed class AddModuleMessage : ClientMessage
    {
        internal override State RequiredState
        {
            get { return State.Active; }
        }

        private int ModuleID { get; set; }
        //private int Resolution { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddModuleMessage"/> class.
        /// </summary>
        /// <param name="station"></param>
        internal AddModuleMessage(Station station) : base(station)
        {

        }

        internal override void Decode(BsonDocument document)
        {
            this.ModuleID = document["module_id"].AsInt32;
            //this.Resolution = document["res"].AsInt32;
        }

        internal override void Execute()
        {
            this.Station.ModuleManager.AddModule(this.ModuleID);
        }
    }
}

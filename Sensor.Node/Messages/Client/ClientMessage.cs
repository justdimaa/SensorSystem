using MongoDB.Bson;

namespace Sensor.Node.Messages.Client
{
    internal abstract class ClientMessage : Message
    {
        internal abstract State RequiredState { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientMessage"/> class.
        /// </summary>
        /// <param name="station"></param>
        protected ClientMessage(Station station) : base(station)
        {

        }

        /// <summary>
        /// Decodes the message.
        /// </summary>
        /// <param name="document"></param>
        internal abstract void Decode(BsonDocument document);

        /// <summary>
        /// Executes the message.
        /// </summary>
        internal abstract void Execute();
    }
}

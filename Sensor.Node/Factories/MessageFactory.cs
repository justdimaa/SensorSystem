using Sensor.Node.Messages.Client;

namespace Sensor.Node
{
    internal static class MessageFactory
    {
        internal static ClientMessage CreateClientMessage(int type, Station station)
        {
            switch (type)
            {
                case 0x64:
                    return new HandshakeMessage(station);
                case 0x69:
                    return new AddModuleMessage(station);
                case 0x6A:
                    return new RemoveModuleMessage(station);
                case 0x6E:
                    return new UpdateModuleInfoMessage(station);
            }

            return null;
        }
    }
}

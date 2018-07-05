using Sensor.Node.Modules;
using Sensor.Node.Modules.Digital;

namespace Sensor.Node.Factories
{
    internal static class ModuleFactory
    {
        /// <summary>
        /// Creates a new module.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="station"></param>
        /// <returns>A new module.</returns>
        internal static Module CreateModule(int type, Station station)
        {
            switch (type)
            {
                case 0x01:
                    return new OpticalDensityModule(station);
                case 0x02:
                    return new TemperatureModule(station);
            }

            return null;
        }
    }
}

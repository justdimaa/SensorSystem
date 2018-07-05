using System;
using System.Collections.Generic;
using System.Linq;
using Sensor.Node.Factories;
using Sensor.Node.Modules;

namespace Sensor.Node.Managers
{
    internal sealed class ModuleManager
    {
        private Station Station { get; }
        private IList<Module> Modules { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="ModuleManager"/> class.
        /// </summary>
        /// <param name="station"></param>
        internal ModuleManager(Station station)
        {
            this.Station = station;
            this.Modules = new List<Module>();
        }

        /// <summary>
        /// Adds a new module to the collection.
        /// </summary>
        /// <param name="type"></param>
        internal void AddModule(int type)
        {
            var module = ModuleFactory.CreateModule(type, this.Station);

            if (module != null)
            {
                this.Modules.Add(module);
            }
        }
        
        internal Module GetModule(int type)
        {
            return this.Modules.ToList().FirstOrDefault(m => m.Type == type);
        }

        [Obsolete("See UpdateModuleInfoMessage.")]
        internal void Tick()
        {
            foreach (var module in this.Modules.ToList())
            {
                module?.Tick();
            }
        }
    }
}

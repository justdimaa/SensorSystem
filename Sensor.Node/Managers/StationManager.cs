using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;

namespace Sensor.Node.Managers
{
    internal sealed class StationManager
    {
        #region Singleton

        private static object ThisLock => new object();

        private static StationManager _instance;
        public static StationManager Instance
        {
            get
            {
                lock (StationManager.ThisLock)
                {
                    if (StationManager._instance == null)
                    {
                        StationManager._instance = new StationManager();
                    }
                }

                return StationManager._instance;
            }
        }

        #endregion

        internal ConcurrentDictionary<string, Station> Stations { get; }
        private Thread RunTickThread { get; set; }
        
        /// <summary>
        /// Initializes the singleton of the <see cref="StationManager"/> class.
        /// </summary>
        private StationManager()
        {
            this.Stations = new ConcurrentDictionary<string, Station>();
        }

        /// <summary>
        /// Initializes the singleton.
        /// </summary>
        public void Initialize()
        {
            this.RunTickThread = new Thread(this.TickLoop);
            this.RunTickThread.Start();
        }

        /// <summary>
        /// Creates a new station.
        /// </summary>
        /// <param name="serialPort"></param>
        /// <returns>The new station.</returns>
        public Station CreateStation(SerialPort serialPort)
        {
            var station = new Station(serialPort);

            if (this.Stations.TryAdd(serialPort.PortName, station))
            {
                return station;
            }

            return null;
        }

        /// <summary>
        /// Removes a station.
        /// </summary>
        /// <param name="station"></param>
        public void RemoveStation(Station station)
        {
            if (this.Stations.TryRemove(station.SerialPort.PortName, out _))
            {
                Debug.WriteLine($"Station {station.Name} disconnected.");
            }
        }

        /// <summary>
        /// Runs a loop to refresh the stations.
        /// </summary>
        internal void TickLoop()
        {
            while (true)
            {
                foreach (var station in this.Stations)
                {
                    station.Value.Tick();
                }

                Thread.Sleep(500);
            }
        }
    }
}

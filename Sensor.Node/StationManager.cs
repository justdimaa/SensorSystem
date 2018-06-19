using System.Collections.Concurrent;
using System.IO.Ports;

namespace Sensor.Node
{
    sealed class StationManager
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

        public ConcurrentDictionary<string, Station> Stations { get; }

        private StationManager()
        {
            this.Stations = new ConcurrentDictionary<string, Station>();
        }

        public void Initialize()
        {

        }

        public Station CreateStation(SerialPort serialPort)
        {
            var station = new Station(serialPort);
            this.Stations.TryAdd(serialPort.PortName, station);
            return station;
        }

        public void RemoveStation(Station station)
        {
            this.Stations.TryRemove(station.SerialPort.PortName, out station);
            station = null;
        }
    }
}

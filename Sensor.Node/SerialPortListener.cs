using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading.Tasks;
using Sensor.Node.Managers;

namespace Sensor.Node
{
    internal sealed class SerialPortListener
    {
        #region Singleton

        private static object ThisLock => new object();

        private static SerialPortListener _instance;
        public static SerialPortListener Instance
        {
            get
            {
                lock (SerialPortListener.ThisLock)
                {
                    if (SerialPortListener._instance == null)
                    {
                        SerialPortListener._instance = new SerialPortListener();
                    }
                }

                return SerialPortListener._instance;
            }
        }

        #endregion

        private List<SerialPort> SerialPorts { get; }
        internal const int Timeout = 5000;

        /// <summary>
        /// Initializes the singleton of the <see cref="SerialPortListener"/> class.
        /// </summary>
        private SerialPortListener()
        {
            this.SerialPorts = new List<SerialPort>();
        }

        internal void Initialize()
        {
            //Task.Run(async () => await this.Listen());
        }

        [Obsolete("Needs a rework.")]
        private async Task Listen()
        {
            while (true)
            {
                this.CheckForNewPorts();

                foreach (var serialPort in this.SerialPorts)
                {
                    this.TryAdd(serialPort);
                }

                await Task.Delay(SerialPortListener.Timeout);
            }
        }

        /// <summary>
        /// Checks all ports for new devices.
        /// </summary>
        private void CheckForNewPorts()
        {
            string[] portNames = SerialPort.GetPortNames();

            foreach (string portName in portNames)
            {
                if (this.SerialPorts.Exists(p => p.PortName == portName)) continue;
                this.SerialPorts.Add(new SerialPort(portName, 115200));
            }
        }

        /// <summary>
        /// Tries to add a new station, if the serial port is not already used.
        /// </summary>
        /// <param name="serialPort"></param>
        internal void TryAdd(SerialPort serialPort)
        {
            Debug.WriteLine(serialPort.PortName);

            if (!StationManager.Instance.Stations.ContainsKey(serialPort.PortName))
            {
                StationManager.Instance.CreateStation(serialPort);
            }
        }
    }
}

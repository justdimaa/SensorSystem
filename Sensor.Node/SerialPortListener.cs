using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;

namespace Sensor.Node
{
    sealed class SerialPortListener
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
        public const int Timeout = 5000;

        private SerialPortListener()
        {
            this.SerialPorts = new List<SerialPort>();
        }

        public void Initialize()
        {
            Task.Run(async () => await this.Listen());
        }

        private async Task Listen()
        {
            /*while (true)
            {
                this.CheckForNewPorts();

                foreach (var serialPort in this.SerialPorts)
                {
                    this.Check(serialPort);
                }

                await Task.Delay(SerialPortListener.Timeout);
            }*/
            this.Check(new SerialPort("COM3", 115200));
        }

        private void CheckForNewPorts()
        {
            string[] portNames = SerialPort.GetPortNames();

            foreach (string portName in portNames)
            {
                if (this.SerialPorts.Exists(p => p.PortName == portName)) continue;
                    this.SerialPorts.Add(new SerialPort(portName, 115200));
            }
        }

        private void Check(SerialPort serialPort)
        {
            Console.WriteLine(serialPort.PortName);
            if (!StationManager.Instance.Stations.ContainsKey(serialPort.PortName))
            {
                StationManager.Instance.CreateStation(serialPort);
            }
            Console.WriteLine(serialPort.PortName);
        }
    }
}

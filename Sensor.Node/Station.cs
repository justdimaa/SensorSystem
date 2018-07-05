using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading.Tasks;
using MongoDB.Bson;
using Sensor.Node.Managers;
using Sensor.Node.Messages.Server;

namespace Sensor.Node
{
    internal sealed class Station
    {
        internal SerialPort SerialPort { get; private set; }
        internal string Name { get; set; }
        internal State State { get; set; }
        internal ModuleManager ModuleManager { get; }
        private string Buffer { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Station"/> class.
        /// </summary>
        /// <param name="serialPort"></param>
        public Station(SerialPort serialPort)
        {
            this.ModuleManager = new ModuleManager(this);
            this.Buffer = string.Empty;
            this.LoadStation(serialPort);
        }

        /// <summary>
        /// Loads the station.
        /// </summary>
        /// <param name="serialPort">The serial port where the station is connected.</param>
        public void LoadStation(SerialPort serialPort)
        {
            this.SerialPort = serialPort;
            this.SerialPort.DataReceived += this.DataReceived;

            try
            {
                this.SerialPort.Open();
                this.State = State.Handshake;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Will be triggered, when new data is incoming.
        /// Handles the data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var serialPort = (SerialPort)sender;
            this.Buffer += serialPort.ReadExisting();

            while (this.Buffer.Contains("\r\n"))
            {
                string buffer = this.Buffer.Split("\r\n")[0];
                this.Buffer = this.Buffer.Remove(0, buffer.Length + 2);

                Task.Run(() =>
                {
                    //Console.WriteLine(buffer);
                    if (BsonDocument.TryParse(buffer, out var document))
                    {
                        int messageID = document["msg_id"].AsInt32;
                        var message = MessageFactory.CreateClientMessage(messageID, this);

#if DEBUG
                        Debug.WriteLine($"Client | {this.Name} | Message received >> {messageID}");
#endif

                        if (message?.RequiredState == this.State)
                        {
                            message.Decode(document);
                            message.Execute();
                        }
                    }
                });
            }
        }

        /// <summary>
        /// Sends a message in json format to the station.
        /// </summary>
        /// <param name="message"></param>
        public void SendData(ServerMessage message)
        {
            message.Encode();
            this.SerialPort.WriteLine(message.Document.ToJson());
            Debug.WriteLine($"Server | {this.Name} | Message sended >> {message.Identifier}");
        }

        /// <summary>
        /// Checks if the port is active.
        /// </summary>
        /// <returns></returns>
        private void CheckConnection()
        {
            if (!this.SerialPort.IsOpen)
            {
                Debug.WriteLine("closed");
                StationManager.Instance.RemoveStation(this);
            }
        }

        public void Tick()
        {
            this.CheckConnection();
            //this.ModuleManager.Tick();
        }
    }

    enum State
    {
        Inactive,
        Handshake,
        Active
    }
}

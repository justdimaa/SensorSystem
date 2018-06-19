using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading.Tasks;
using MongoDB.Bson;
using Sensor.Node.Messages.Client;
using Sensor.Node.Messages.Server;
using Sensor.Node.Modules;

namespace Sensor.Node
{
    class Station
    {
        public SerialPort SerialPort { get; private set; }
        public string Name { get; set; }
        public State State { get; set; }
        public List<Module> Modules { get; }
        private string Buffer { get; set; }

        public Station(SerialPort serialPort)
        {
            this.Modules = new List<Module>();
            this.Buffer = string.Empty;
            this.LoadStation(serialPort);
        }

        public void LoadStation(SerialPort serialPort)
        {
            this.Modules.Clear();
            this.SerialPort = serialPort;
            this.SerialPort.DataReceived += this.DataReceived;

            try
            {
                this.SerialPort.Open();
                this.State = State.Handshake;
                Task.Run(async () => await this.RunPortChecker());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var serialPort = (SerialPort) sender;
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
                        ClientMessage message = null;

#if DEBUG
                        Debug.WriteLine($"Client | {this.Name} | Message received >> {messageID}");
#endif

                        switch (messageID)
                        {
                            case 0x64:
                                message = new HandshakeMessage(this);
                                break;
                            case 0x69:
                                message = new AddModuleMessage(this);
                                break;
                            case 0x6A:
                                message = new RemoveModuleMessage(this);
                                break;
                            case 0x6E:
                                message = new UpdateModuleInfoMessage(this);
                                break;
                        }

                        if (message?.RequiredState == this.State)
                        {
                            message?.Decode(document);
                            message?.Execute();
                        }
                    }
                });
            }
        }

        public void SendData(ServerMessage message)
        {
            message.Encode();

            this.SerialPort.WriteLine(message.Document.ToJson());

#if DEBUG
            Debug.WriteLine($"Server | {this.Name} | Message sended >> {message.Identifier}");
#endif
        }

        private async Task RunPortChecker()
        {
            while (true)
            {
                if (!this.SerialPort.IsOpen)
                {
                    Console.WriteLine("closed");
                }

                await Task.Delay(500);
            }
        }

        public void Tick()
        {

        }
    }
}

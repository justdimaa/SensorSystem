using System.IO.Ports;
using System.Threading.Tasks;
using Sensor.Node.Managers;

namespace Sensor.Node
{
    internal sealed class Program
    {
        private static async Task Main()
        {
            StationManager.Instance.Initialize();
            SerialPortListener.Instance.Initialize();

            SerialPortListener.Instance.TryAdd(new SerialPort("COM3", 115200)); // Bypass

            await Task.Delay(-1);
        }
    }
}

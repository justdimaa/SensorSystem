using System.Threading.Tasks;

namespace Sensor.Node
{
    class Program
    {
        private static void Main() => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            StationManager.Instance.Initialize();
            SerialPortListener.Instance.Initialize();

            await Task.Delay(-1);
        }
    }
}

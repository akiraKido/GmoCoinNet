using GmoCoinNet.Examples.Abstractions;

namespace GmoCoinNet.Examples.Examples;

public class ServiceStatusExample(GmoCoinPublicApi api) : IApiExample
{
    public string Name => "Get Service Status";
    public string Description => "Checks the current status of the GMO Coin service.";

    public async Task RunAsync()
    {
        var status = await api.GetStatusAsync();
        Console.WriteLine($"GMO Coin service status: {status.Data.Status}");
    }
} 
using GmoCoinNet;
using GmoCoinNet.Schema;

namespace GmoCoinNet.Examples;

public class Program
{
    public static async Task Main()
    {
        var publicApi = new GmoCoinPublicApi();
        var webSocket = new GmoCoinPublicWebSocket();

        // Get current status
        var status = await publicApi.GetStatusAsync();
        Console.WriteLine($"GMO Coin service status: {status.Data.Status}");

        // Get BTC ticker
        var ticker = await publicApi.GetTickerAsync(Ticker.Btc);
        var bitcoinData = ticker.Data[0];
        Console.WriteLine($"BTC price: Ask={bitcoinData.Ask}, Bid={bitcoinData.Bid}");

        // Subscribe to BTC ticker updates
        Console.WriteLine("Subscribing to BTC ticker updates (press any key to exit)...");
        
        using var cancellationTokenSource = new CancellationTokenSource();
        var subscription = Task.Run(async () =>
        {
            await foreach (var update in webSocket.SubscribeToTickerStream(Ticker.Btc, cancellationTokenSource.Token))
            {
                Console.WriteLine($"BTC update: Ask={update.Ask}, Bid={update.Bid}, Time={update.Timestamp:HH:mm:ss.fff}");
            }
        });

        Console.ReadKey();
        await cancellationTokenSource.CancelAsync();
        await subscription;
    }
}

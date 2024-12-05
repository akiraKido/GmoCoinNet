using GmoCoinNet.Examples.Abstractions;
using GmoCoinNet.Schema;

namespace GmoCoinNet.Examples.Examples;

public class BtcTickerStreamExample(GmoCoinPublicWebSocket webSocket) : IApiExample
{
    public string Name => "Subscribe to BTC Ticker Updates";
    public string Description => "Streams real-time BTC price updates. Press any key to stop.";

    public async Task RunAsync()
    {
        using var cancellationTokenSource = new CancellationTokenSource();
        var subscription = Task.Run(async () =>
        {
            await foreach (var update in webSocket.SubscribeToTickerStream(Ticker.Btc, cancellationTokenSource.Token))
            {
                Console.WriteLine($"BTC update: Ask={update.Ask}, Bid={update.Bid}, Time={update.Timestamp:HH:mm:ss.fff}");
            }
        }, cancellationTokenSource.Token);

        Console.ReadKey(true);
        await cancellationTokenSource.CancelAsync();
        await subscription;
    }
} 
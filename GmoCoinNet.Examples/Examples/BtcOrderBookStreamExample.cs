using GmoCoinNet.Examples.Abstractions;
using GmoCoinNet.Schema;

namespace GmoCoinNet.Examples.Examples;

public class BtcOrderBookStreamExample : IApiExample
{
    private readonly GmoCoinPublicWebSocket _ws;
    public string Name => "Subscribe to BTC Order Book Updates";
    public string Description => "Streams real-time BTC order book updates. Press any key to stop.";

    public BtcOrderBookStreamExample(GmoCoinPublicWebSocket ws) => _ws = ws;

    public async Task RunAsync()
    {
        using var cts = new CancellationTokenSource();
        var subscription = Task.Run(async () =>
        {
            await foreach (var update in _ws.SubscribeToOrderBooks(Ticker.Btc, cts.Token))
            {
                Console.WriteLine($"Order Book update at {update.Timestamp:HH:mm:ss.fff}:");
                Console.WriteLine($"Best Ask: {update.Asks[0].Price} ({update.Asks[0].Size})");
                Console.WriteLine($"Best Bid: {update.Bids[0].Price} ({update.Bids[0].Size})");
                Console.WriteLine();
            }
        });

        Console.ReadKey(true);
        await cts.CancelAsync();
        await subscription;
    }
} 
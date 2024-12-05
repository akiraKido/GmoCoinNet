using GmoCoinNet.Examples.Abstractions;
using GmoCoinNet.Schema;

namespace GmoCoinNet.Examples.Examples;

public class BtcTradesExample(GmoCoinPublicApi api) : IApiExample
{
    public string Name => "Get BTC Trade History";
    public string Description => "Gets the recent BTC trade history (last 10 trades).";

    public async Task RunAsync()
    {
        var trades = await api.GetTradesAsync(Ticker.Btc, count: 10);
        Console.WriteLine("Recent BTC Trades:");
        foreach (var trade in trades.Data.List)
        {
            Console.WriteLine($"Time: {trade.Timestamp:HH:mm:ss.fff}, " +
                            $"Price: {trade.Price}, " +
                            $"Size: {trade.Size}, " +
                            $"Side: {trade.Side}");
        }
    }
} 
using GmoCoinNet.Examples.Abstractions;
using GmoCoinNet.Schema;

namespace GmoCoinNet.Examples.Examples;

public class BtcOrderBookExample(GmoCoinPublicApi api) : IApiExample
{
    public string Name => "Get BTC Order Book";
    public string Description => "Gets the current BTC order book (market depth).";

    public async Task RunAsync()
    {
        var orderBook = await api.GetOrderBooksAsync(Ticker.Btc);
        Console.WriteLine("BTC Order Book:");
        Console.WriteLine("\nTop 5 Ask Orders (Sell):");
        foreach (var ask in orderBook.Data.Asks.Take(5))
        {
            Console.WriteLine($"Price: {ask.Price}, Size: {ask.Size}");
        }
        
        Console.WriteLine("\nTop 5 Bid Orders (Buy):");
        foreach (var bid in orderBook.Data.Bids.Take(5))
        {
            Console.WriteLine($"Price: {bid.Price}, Size: {bid.Size}");
        }
    }
} 
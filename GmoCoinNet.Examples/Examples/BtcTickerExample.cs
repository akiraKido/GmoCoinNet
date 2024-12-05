using GmoCoinNet.Examples.Abstractions;
using GmoCoinNet.Schema;

namespace GmoCoinNet.Examples.Examples;

public class BtcTickerExample(GmoCoinPublicApi api) : IApiExample
{
    public string Name => "Get BTC Ticker";
    public string Description => "Gets the current BTC price information.";

    public async Task RunAsync()
    {
        var ticker = await api.GetTickerAsync(Ticker.Btc);
        var bitcoinData = ticker.Data[0];
        Console.WriteLine($"BTC price: Ask={bitcoinData.Ask}, Bid={bitcoinData.Bid}");
    }
} 
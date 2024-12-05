using GmoCoinNet.Examples.Abstractions;
using GmoCoinNet.Schema;

namespace GmoCoinNet.Examples.Examples;

public class BtcKLinesExample(GmoCoinPublicApi api) : IApiExample
{
    public string Name => "Get BTC Daily Candlesticks";
    public string Description => "Gets the BTC daily candlestick data for the current year.";

    public async Task RunAsync()
    {
        var year = DateTime.UtcNow.Year.ToString();
        var klines = await api.GetKLinesAsync(Ticker.Btc, KLineInterval.OneDay, year);
        
        Console.WriteLine($"BTC Daily Candlesticks for {year}:");
        foreach (var candle in klines.Data.Take(5))
        {
            Console.WriteLine($"Date: {candle.OpenTime:yyyy-MM-dd}, " +
                            $"Open: {candle.Open}, " +
                            $"High: {candle.High}, " +
                            $"Low: {candle.Low}, " +
                            $"Close: {candle.Close}, " +
                            $"Volume: {candle.Volume}");
        }
    }
} 
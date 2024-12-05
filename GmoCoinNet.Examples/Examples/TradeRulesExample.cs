using GmoCoinNet.Examples.Abstractions;

namespace GmoCoinNet.Examples.Examples;

public class TradeRulesExample(GmoCoinPublicApi api) : IApiExample
{
    public string Name => "Get Trading Rules";
    public string Description => "Gets the trading rules for all available symbols.";

    public async Task RunAsync()
    {
        var rules = await api.GetTradeRulesAsync();
        Console.WriteLine("Trading Rules:");
        foreach (var rule in rules.Data)
        {
            Console.WriteLine($"\nSymbol: {rule.Symbol}");
            Console.WriteLine($"Min Order Size: {rule.MinOrderSize}");
            Console.WriteLine($"Max Order Size: {rule.MaxOrderSize}");
            Console.WriteLine($"Size Step: {rule.SizeStep}");
            Console.WriteLine($"Tick Size: {rule.TickSize}");
            Console.WriteLine($"Maker Fee: {rule.MakerFee}");
            Console.WriteLine($"Taker Fee: {rule.TakerFee}");
        }
    }
} 
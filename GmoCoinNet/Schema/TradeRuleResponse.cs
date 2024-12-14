using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GmoCoinNet.Schema;

/// <summary>Trading rules for a symbol</summary>
/// <summary xml:lang="ja">銘柄の取引ルール</summary>
public class TradeRule
{
    /// <summary>Trading pair symbol</summary>
    /// <summary xml:lang="ja">取引ペアのシンボル</summary>
    [JsonProperty("symbol")] public readonly Ticker Symbol;

    /// <summary>Minimum order size per transaction</summary>
    /// <summary xml:lang="ja">最小注文数量/回</summary>
    [JsonProperty("minOrderSize")] public readonly decimal MinOrderSize;

    /// <summary>Maximum order size per transaction</summary>
    /// <summary xml:lang="ja">最大注文数量/回</summary>
    [JsonProperty("maxOrderSize")] public readonly decimal MaxOrderSize;

    /// <summary>Minimum order size increment</summary>
    /// <summary xml:lang="ja">最小注文単位/回</summary>
    [JsonProperty("sizeStep")] public readonly decimal SizeStep;

    /// <summary>Minimum price increment</summary>
    /// <summary xml:lang="ja">注文価格の呼値</summary>
    [JsonProperty("tickSize")] public readonly decimal TickSize;

    /// <summary>Taker fee rate</summary>
    /// <summary xml:lang="ja">taker手数料</summary>
    [JsonProperty("takerFee")] public readonly decimal TakerFee;

    /// <summary>Maker fee rate</summary>
    /// <summary xml:lang="ja">maker手数料</summary>
    [JsonProperty("makerFee")] public readonly decimal MakerFee;

    [JsonConstructor]
    internal TradeRule(
        [JsonProperty("symbol")] string symbol,
        [JsonProperty("minOrderSize")] string minOrderSize,
        [JsonProperty("maxOrderSize")] string maxOrderSize,
        [JsonProperty("sizeStep")] string sizeStep,
        [JsonProperty("tickSize")] string tickSize,
        [JsonProperty("takerFee")] string takerFee,
        [JsonProperty("makerFee")] string makerFee)
    {
        Symbol = TickerService.FromString(symbol);
        MinOrderSize = decimal.Parse(minOrderSize);
        MaxOrderSize = decimal.Parse(maxOrderSize);
        SizeStep = decimal.Parse(sizeStep);
        TickSize = decimal.Parse(tickSize);
        TakerFee = decimal.Parse(takerFee);
        MakerFee = decimal.Parse(makerFee);
    }
}
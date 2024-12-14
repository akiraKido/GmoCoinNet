using System;
using Newtonsoft.Json;

namespace GmoCoinNet.Schema;

/// <summary>
/// Represents a single ticker entry containing volume and price information for a trading pair
/// </summary>
/// <summary xml:lang="ja">
/// 取引ペアの出来高と価格情報を含むティッカーエントリを表します
/// </summary>
public class VolumeEntry
{
    /// <summary>Ask price</summary>
    /// <summary xml:lang="ja">売値</summary>
    [JsonProperty("ask")] public readonly decimal Ask;

    /// <summary>Bid price</summary>
    /// <summary xml:lang="ja">買値</summary>
    [JsonProperty("bid")] public readonly decimal Bid;

    /// <summary>Highest price</summary>
    /// <summary xml:lang="ja">最高値</summary>
    [JsonProperty("high")] public readonly decimal High;

    /// <summary>Last traded price</summary>
    /// <summary xml:lang="ja">最終取引価格</summary>
    [JsonProperty("last")] public readonly decimal Last;

    /// <summary>Lowest price</summary>
    /// <summary xml:lang="ja">最安値</summary>
    [JsonProperty("low")] public readonly decimal Low;

    /// <summary>Trading pair symbol</summary>
    /// <summary xml:lang="ja">取引ペアのシンボル</summary>
    [JsonProperty("symbol")] public readonly Ticker Symbol;

    /// <summary>Timestamp of the ticker data</summary>
    /// <summary xml:lang="ja">ティッカーデータのタイムスタンプ</summary>
    [JsonProperty("timestamp")] public readonly DateTime Timestamp;

    /// <summary>Trading volume</summary>
    /// <summary xml:lang="ja">取引量</summary>
    [JsonProperty("volume")] public readonly decimal Volume;

    [JsonConstructor]
    internal VolumeEntry(
        string ask, string bid, string high, string last, string low, string symbol, DateTime timestamp, string volume)
    {
        Ask = decimal.Parse(ask);
        Bid = decimal.Parse(bid);
        High = decimal.Parse(high);
        Last = decimal.Parse(last);
        Low = decimal.Parse(low);
        Symbol = TickerService.FromString(symbol);
        Timestamp = timestamp;
        Volume = decimal.Parse(volume);
    }
}

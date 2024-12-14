using System;
using Newtonsoft.Json;

namespace GmoCoinNet.Schema;

/// <summary>Real-time ticker information for a trading pair</summary>
/// <summary xml:lang="ja">取引ペアのリアルタイムティッカー情報</summary>
public class TickerEntry
{
    /// <summary>Channel name (always "ticker")</summary>
    /// <summary xml:lang="ja">チャンネル名（常に"ticker"）</summary>
    [JsonProperty("channel")] public readonly string Channel;

    /// <summary>Current best ask price</summary>
    /// <summary xml:lang="ja">現在の売注文の最良気配値</summary>
    [JsonProperty("ask")] public readonly decimal Ask;

    /// <summary>Current best bid price</summary>
    /// <summary xml:lang="ja">現在の買注文の最良気配値</summary>
    [JsonProperty("bid")] public readonly decimal Bid;

    /// <summary>Highest price of the day (based on last price)</summary>
    /// <summary xml:lang="ja">当日の最高値(最終取引価格)</summary>
    [JsonProperty("high")] public readonly decimal High;

    /// <summary>Last traded price</summary>
    /// <summary xml:lang="ja">最終取��価格</summary>
    [JsonProperty("last")] public readonly decimal Last;

    /// <summary>Lowest price of the day (based on last price)</summary>
    /// <summary xml:lang="ja">当日の最安値(最終取引価格)</summary>
    [JsonProperty("low")] public readonly decimal Low;

    /// <summary>Trading pair symbol</summary>
    /// <summary xml:lang="ja">取引ペアのシンボル</summary>
    [JsonProperty("symbol")] public readonly Ticker Symbol;

    /// <summary>Timestamp of the last trade</summary>
    /// <summary xml:lang="ja">約定時のタイムスタンプ</summary>
    [JsonProperty("timestamp")] public readonly DateTime Timestamp;

    /// <summary>24-hour trading volume</summary>
    /// <summary xml:lang="ja">24時間の取引量</summary>
    [JsonProperty("volume")] public readonly decimal Volume;

    [JsonConstructor]
    internal TickerEntry(
        string channel,
        [JsonProperty("ask")] string ask,
        [JsonProperty("bid")] string bid,
        [JsonProperty("high")] string high,
        [JsonProperty("last")] string last,
        [JsonProperty("low")] string low,
        string symbol,
        DateTime timestamp,
        [JsonProperty("volume")] string volume)
    {
        Channel = channel;
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
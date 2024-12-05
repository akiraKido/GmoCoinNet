using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GmoCoinNet.Schema;

/// <summary>Real-time order book update for a trading pair</summary>
/// <summary xml:lang="ja">取引ペアのリアルタイム板情報更新</summary>
public class OrderBookStreamEntry
{
    /// <summary>Channel name (always "orderbooks")</summary>
    /// <summary xml:lang="ja">チャンネル名（常に"orderbooks"）</summary>
    [JsonProperty("channel")] public readonly string Channel;

    /// <summary>List of ask (sell) orders</summary>
    /// <summary xml:lang="ja">売り注文のリスト（最良気配から昇順）</summary>
    [JsonProperty("asks")] public readonly IReadOnlyList<OrderBookEntry> Asks;

    /// <summary>List of bid (buy) orders</summary>
    /// <summary xml:lang="ja">買い注文のリスト（最良気配から降順）</summary>
    [JsonProperty("bids")] public readonly IReadOnlyList<OrderBookEntry> Bids;

    /// <summary>Trading pair symbol</summary>
    /// <summary xml:lang="ja">取引ペアのシンボル</summary>
    [JsonProperty("symbol")] public readonly string Symbol;

    /// <summary>Timestamp of the update</summary>
    /// <summary xml:lang="ja">配信日時</summary>
    [JsonProperty("timestamp")] public readonly DateTime Timestamp;

    [JsonConstructor]
    internal OrderBookStreamEntry(
        string channel,
        IReadOnlyList<OrderBookEntry> asks,
        IReadOnlyList<OrderBookEntry> bids,
        string symbol,
        DateTime timestamp)
    {
        Channel = channel;
        Asks = asks;
        Bids = bids;
        Symbol = symbol;
        Timestamp = timestamp;
    }
} 
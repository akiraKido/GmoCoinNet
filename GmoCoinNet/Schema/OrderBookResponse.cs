using System.Collections.Generic;
using Newtonsoft.Json;

namespace GmoCoinNet.Schema;

/// <summary>Response containing order book information for a trading pair</summary>
/// <summary xml:lang="ja">取引ペアの板情報を含むレスポンス</summary>
public class OrderBookResponse
{
    /// <summary>List of ask (sell) orders</summary>
    /// <summary xml:lang="ja">売り注文のリスト</summary>
    [JsonProperty("asks")] public readonly IReadOnlyList<OrderBookEntry> Asks;

    /// <summary>List of bid (buy) orders</summary>
    /// <summary xml:lang="ja">買い注文のリスト</summary>
    [JsonProperty("bids")] public readonly IReadOnlyList<OrderBookEntry> Bids;

    /// <summary>Trading pair symbol</summary>
    /// <summary xml:lang="ja">取引ペアのシンボル</summary>
    [JsonProperty("symbol")] public readonly Ticker Symbol;

    [JsonConstructor]
    internal OrderBookResponse(IReadOnlyList<OrderBookEntry> asks, IReadOnlyList<OrderBookEntry> bids, string symbol)
    {
        Asks = asks;
        Bids = bids;
        Symbol = TickerService.FromString(symbol);
    }
}

/// <summary>Single entry in the order book containing price and size</summary>
/// <summary xml:lang="ja">価格と数量を含む板の1エントリ</summary>
public class OrderBookEntry
{
    /// <summary>Price of the order</summary>
    /// <summary xml:lang="ja">注文の価格</summary>
    [JsonProperty("price")] public readonly decimal Price;

    /// <summary>Size of the order</summary>
    /// <summary xml:lang="ja">注文の数量</summary>
    [JsonProperty("size")] public readonly decimal Size;

    [JsonConstructor]
    internal OrderBookEntry(
        [JsonProperty("price")] string price,
        [JsonProperty("size")] string size)
    {
        Price = decimal.Parse(price);
        Size = decimal.Parse(size);
    }
} 
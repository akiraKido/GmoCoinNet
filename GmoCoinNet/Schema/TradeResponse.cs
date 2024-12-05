using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GmoCoinNet.Schema;

/// <summary>Response containing trade history information</summary>
/// <summary xml:lang="ja">取引履歴情報を含むレスポンス</summary>
public class TradeResponse
{
    /// <summary>Pagination information</summary>
    /// <summary xml:lang="ja">ページング情報</summary>
    [JsonProperty("pagination")] public readonly PaginationInfo Pagination;

    /// <summary>List of trades</summary>
    /// <summary xml:lang="ja">取引履歴のリスト</summary>
    [JsonProperty("list")] public readonly IReadOnlyList<TradeEntry> List;

    [JsonConstructor]
    internal TradeResponse(PaginationInfo pagination, IReadOnlyList<TradeEntry> list)
    {
        Pagination = pagination;
        List = list;
    }
}

/// <summary>Pagination information for the trade history</summary>
/// <summary xml:lang="ja">取引履歴のページング情報</summary>
public class PaginationInfo
{
    /// <summary>Current page number</summary>
    /// <summary xml:lang="ja">現在のページ番号</summary>
    [JsonProperty("currentPage")] public readonly int CurrentPage;

    /// <summary>Number of items per page</summary>
    /// <summary xml:lang="ja">1ページあたりの件数</summary>
    [JsonProperty("count")] public readonly int Count;

    [JsonConstructor]
    internal PaginationInfo(int currentPage, int count)
    {
        CurrentPage = currentPage;
        Count = count;
    }
}

/// <summary>Single trade entry containing execution details</summary>
/// <summary xml:lang="ja">約定詳細を含む取引エントリ</summary>
public class TradeEntry
{
    /// <summary>Execution price</summary>
    /// <summary xml:lang="ja">約定価格</summary>
    [JsonProperty("price")] public readonly decimal Price;

    /// <summary>Trade side (buy/sell)</summary>
    /// <summary xml:lang="ja">売買区分</summary>
    [JsonProperty("side")] public readonly TradeSide Side;

    /// <summary>Execution size</summary>
    /// <summary xml:lang="ja">約定数量</summary>
    [JsonProperty("size")] public readonly decimal Size;

    /// <summary>Execution timestamp</summary>
    /// <summary xml:lang="ja">約定日時</summary>
    [JsonProperty("timestamp")] public readonly DateTime Timestamp;

    [JsonConstructor]
    internal TradeEntry(
        [JsonProperty("price")] string price,
        [JsonProperty("side")] string side,
        [JsonProperty("size")] string size,
        DateTime timestamp)
    {
        Price = decimal.Parse(price);
        Side = side switch
        {
            "BUY" => TradeSide.Buy,
            "SELL" => TradeSide.Sell,
            _ => throw new ArgumentException($"Invalid trade side: {side}")
        };
        Size = decimal.Parse(size);
        Timestamp = timestamp;
    }
}

/// <summary>Trade side (buy/sell)</summary>
/// <summary xml:lang="ja">取引の売買区分</summary>
public enum TradeSide
{
    /// <summary>Buy order</summary>
    /// <summary xml:lang="ja">買い注文</summary>
    Buy,
    
    /// <summary>Sell order</summary>
    /// <summary xml:lang="ja">売り注文</summary>
    Sell
} 
using System;
using Newtonsoft.Json;

namespace GmoCoinNet.Schema;

/// <summary>Candlestick/KLine data for a trading pair</summary>
/// <summary xml:lang="ja">取引ペアのローソク足/KLineデータ</summary>
public class KLineEntry
{
    /// <summary>Opening time in Unix timestamp milliseconds</summary>
    /// <summary xml:lang="ja">開始時刻のUnixタイムスタンプ（ミリ秒）</summary>
    [JsonProperty("openTime")] public readonly DateTime OpenTime;

    /// <summary>Opening price</summary>
    /// <summary xml:lang="ja">始値</summary>
    [JsonProperty("open")] public readonly decimal Open;

    /// <summary>Highest price</summary>
    /// <summary xml:lang="ja">高値</summary>
    [JsonProperty("high")] public readonly decimal High;

    /// <summary>Lowest price</summary>
    /// <summary xml:lang="ja">安値</summary>
    [JsonProperty("low")] public readonly decimal Low;

    /// <summary>Closing price</summary>
    /// <summary xml:lang="ja">終値</summary>
    [JsonProperty("close")] public readonly decimal Close;

    /// <summary>Trading volume</summary>
    /// <summary xml:lang="ja">取引量</summary>
    [JsonProperty("volume")] public readonly decimal Volume;

    [JsonConstructor]
    internal KLineEntry(
        [JsonProperty("openTime")] string openTime,
        [JsonProperty("open")] string open,
        [JsonProperty("high")] string high,
        [JsonProperty("low")] string low,
        [JsonProperty("close")] string close,
        [JsonProperty("volume")] string volume)
    {
        OpenTime = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(openTime)).DateTime;
        Open = decimal.Parse(open);
        High = decimal.Parse(high);
        Low = decimal.Parse(low);
        Close = decimal.Parse(close);
        Volume = decimal.Parse(volume);
    }
}

/// <summary>Available intervals for KLine data</summary>
/// <summary xml:lang="ja">KLineデータの利用可能な時間間隔</summary>
public enum KLineInterval
{
    /// <summary>1 minute interval</summary>
    /// <summary xml:lang="ja">1分足</summary>
    OneMinute,
    /// <summary>5 minute interval</summary>
    /// <summary xml:lang="ja">5分足</summary>
    FiveMinutes,
    /// <summary>10 minute interval</summary>
    /// <summary xml:lang="ja">10分足</summary>
    TenMinutes,
    /// <summary>15 minute interval</summary>
    /// <summary xml:lang="ja">15分足</summary>
    FifteenMinutes,
    /// <summary>30 minute interval</summary>
    /// <summary xml:lang="ja">30分足</summary>
    ThirtyMinutes,
    /// <summary>1 hour interval</summary>
    /// <summary xml:lang="ja">1時間足</summary>
    OneHour,
    /// <summary>4 hour interval</summary>
    /// <summary xml:lang="ja">4時間足</summary>
    FourHours,
    /// <summary>8 hour interval</summary>
    /// <summary xml:lang="ja">8時間足</summary>
    EightHours,
    /// <summary>12 hour interval</summary>
    /// <summary xml:lang="ja">12時間足</summary>
    TwelveHours,
    /// <summary>1 day interval</summary>
    /// <summary xml:lang="ja">日足</summary>
    OneDay,
    /// <summary>1 week interval</summary>
    /// <summary xml:lang="ja">週足</summary>
    OneWeek,
    /// <summary>1 month interval</summary>
    /// <summary xml:lang="ja">月足</summary>
    OneMonth
}

internal static class KLineIntervalExtensions
{
    public static string ToApiString(this KLineInterval interval) => interval switch
    {
        KLineInterval.OneMinute => "1min",
        KLineInterval.FiveMinutes => "5min",
        KLineInterval.TenMinutes => "10min",
        KLineInterval.FifteenMinutes => "15min",
        KLineInterval.ThirtyMinutes => "30min",
        KLineInterval.OneHour => "1hour",
        KLineInterval.FourHours => "4hour",
        KLineInterval.EightHours => "8hour",
        KLineInterval.TwelveHours => "12hour",
        KLineInterval.OneDay => "1day",
        KLineInterval.OneWeek => "1week",
        KLineInterval.OneMonth => "1month",
        _ => throw new ArgumentOutOfRangeException(nameof(interval), interval, null)
    };
} 
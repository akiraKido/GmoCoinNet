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
    [JsonProperty("ask")] public readonly string Ask;

    /// <summary>Bid price</summary>
    /// <summary xml:lang="ja">買値</summary>
    [JsonProperty("bid")] public readonly string Bid;

    /// <summary>Highest price</summary>
    /// <summary xml:lang="ja">最高値</summary>
    [JsonProperty("high")] public readonly string High;

    /// <summary>Last traded price</summary>
    /// <summary xml:lang="ja">最終取引価格</summary>
    [JsonProperty("last")] public readonly string Last;

    /// <summary>Lowest price</summary>
    /// <summary xml:lang="ja">最安値</summary>
    [JsonProperty("low")] public readonly string Low;

    /// <summary>Trading pair symbol</summary>
    /// <summary xml:lang="ja">取引ペアのシンボル</summary>
    [JsonProperty("symbol")] public readonly string Symbol;

    /// <summary>Timestamp of the ticker data</summary>
    /// <summary xml:lang="ja">ティッカーデータのタイムスタンプ</summary>
    [JsonProperty("timestamp")] public readonly DateTime Timestamp;

    /// <summary>Trading volume</summary>
    /// <summary xml:lang="ja">取引量</summary>
    [JsonProperty("volume")] public readonly string Volume;

    [JsonConstructor]
    internal VolumeEntry(
        string ask, string bid, string high, string last, string low, string symbol, DateTime timestamp, string volume)
    {
        Ask = ask;
        Bid = bid;
        High = high;
        Last = last;
        Low = low;
        Symbol = symbol;
        Timestamp = timestamp;
        Volume = volume;
    }
}

//  取扱銘柄名
/// <summary>Available cryptocurrency trading pairs on GMO Coin</summary>
/// <summary xml:lang="ja">GMOコインで取引可能な暗号資産ペア</summary>
public enum Ticker
{
    #region Spot Trading / 現物取引

    /// <summary>Bitcoin</summary>
    Btc,
    /// <summary>Ethereum</summary>
    Eth,
    /// <summary>Bitcoin Cash</summary>
    Bch,
    /// <summary>Litecoin</summary>
    Ltc,
    /// <summary>Ripple</summary>
    Xrp,
    /// <summary>NEM</summary>
    Xem,
    /// <summary>Stellar Lumens</summary>
    Xlm,
    /// <summary>Basic Attention Token</summary>
    Bat,
    /// <summary>Tezos</summary>
    Xtz,
    /// <summary>Qtum</summary>
    Qtum,
    /// <summary>Enjin Coin</summary>
    Enj,
    /// <summary>Polkadot</summary>
    Dot,
    /// <summary>Cosmos</summary>
    Atom,
    /// <summary>Maker</summary>
    Mkr,
    /// <summary>Dai</summary>
    Dai,
    /// <summary>Symbol</summary>
    Xym,
    /// <summary>MonaCoin</summary>
    Mona,
    /// <summary>FCR</summary>
    Fcr,
    /// <summary>Cardano</summary>
    Ada,
    /// <summary>Chainlink</summary>
    Link,
    /// <summary>Dogecoin</summary>
    Doge,
    /// <summary>Solana</summary>
    Sol,
    /// <summary>Astar</summary>
    Astr,
    
    #endregion

    #region Leveraged Trading / レバレッジ取引

    /// <summary>Bitcoin/Japanese Yen pair for leveraged trading</summary>
    /// <summary xml:lang="ja">ビットコイン/日本円 レバレッジ取引ペア</summary>
    BtcJpy,
    /// <summary>Ethereum/Japanese Yen pair for leveraged trading</summary>
    /// <summary xml:lang="ja">イーサリアム/日本円 レバレッジ取引ペア</summary>
    EthJpy,
    /// <summary>Bitcoin Cash/Japanese Yen pair for leveraged trading</summary>
    /// <summary xml:lang="ja">ビットコインキャッシュ/日本円 レバレッジ取引ペア</summary>
    BchJpy,
    /// <summary>Litecoin/Japanese Yen pair for leveraged trading</summary>
    /// <summary xml:lang="ja">ライトコイン/日本円 レバレッジ取引ペア</summary>
    LtcJpy,
    /// <summary>Ripple/Japanese Yen pair for leveraged trading</summary>
    /// <summary xml:lang="ja">リップル/日本円 レバレッジ取引ペア</summary>
    XrpJpy,
    /// <summary>Polkadot/Japanese Yen pair for leveraged trading</summary>
    /// <summary xml:lang="ja">ポルカドット/日本円 レバレッジ取引ペア</summary>
    DotJpy,
    /// <summary>Cosmos/Japanese Yen pair for leveraged trading</summary>
    /// <summary xml:lang="ja">コスモス/日本円 レバレッジ取引ペア</summary>
    AtomJpy,
    /// <summary>Cardano/Japanese Yen pair for leveraged trading</summary>
    /// <summary xml:lang="ja">カルダノ/日本円 レバレッジ取引ペア</summary>
    AdaJpy,
    /// <summary>Chainlink/Japanese Yen pair for leveraged trading</summary>
    /// <summary xml:lang="ja">チェーンリンク/日本円 レバレッジ取引ペア</summary>
    LinkJpy,
    /// <summary>Dogecoin/Japanese Yen pair for leveraged trading</summary>
    /// <summary xml:lang="ja">ドージコイン/日本円 レバレッジ取引ペア</summary>
    DogeJpy,
    /// <summary>Solana/Japanese Yen pair for leveraged trading</summary>
    /// <summary xml:lang="ja">ソラナ/日本円 レバレッジ取引ペア</summary>
    SolJpy,
    /// <summary>Astar/Japanese Yen pair for leveraged trading</summary>
    /// <summary xml:lang="ja">アスター/日本円 レバレッジ取引ペア</summary>
    AstrJpy
    
    #endregion
}

internal static class TickerService
{
    private const string Btc = "BTC";
    private const string Eth = "ETH";
    private const string Bch = "BCH";
    private const string Ltc = "LTC";
    private const string Xrp = "XRP";
    private const string Xem = "XEM";
    private const string Xlm = "XLM";
    private const string Bat = "BAT";
    private const string Xtz = "XTZ";
    private const string Qtum = "QTUM";
    private const string Enj = "ENJ";
    private const string Dot = "DOT";
    private const string Atom = "ATOM";
    private const string Mkr = "MKR";
    private const string Dai = "DAI";
    private const string Xym = "XYM";
    private const string Mona = "MONA";
    private const string Fcr = "FCR";
    private const string Ada = "ADA";
    private const string Link = "LINK";
    private const string Doge = "DOGE";
    private const string Sol = "SOL";
    private const string Astr = "ASTR";
    private const string BtcJpy = "BTC_JPY";
    private const string EthJpy = "ETH_JPY";
    private const string BchJpy = "BCH_JPY";
    private const string LtcJpy = "LTC_JPY";
    private const string XrpJpy = "XRP_JPY";
    private const string DotJpy = "DOT_JPY";
    private const string AtomJpy = "ATOM_JPY";
    private const string AdaJpy = "ADA_JPY";
    private const string LinkJpy = "LINK_JPY";
    private const string DogeJpy = "DOGE_JPY";
    private const string SolJpy = "SOL_JPY";
    private const string AstrJpy = "ASTR_JPY";
    
    public static string ToString(Ticker ticker) =>
        ticker switch
        {
            Ticker.Btc => Btc,
            Ticker.Eth => Eth,
            Ticker.Bch => Bch,
            Ticker.Ltc => Ltc,
            Ticker.Xrp => Xrp,
            Ticker.Xem => Xem,
            Ticker.Xlm => Xlm,
            Ticker.Bat => Bat,
            Ticker.Xtz => Xtz,
            Ticker.Qtum => Qtum,
            Ticker.Enj => Enj,
            Ticker.Dot => Dot,
            Ticker.Atom => Atom,
            Ticker.Mkr => Mkr,
            Ticker.Dai => Dai,
            Ticker.Xym => Xym,
            Ticker.Mona => Mona,
            Ticker.Fcr => Fcr,
            Ticker.Ada => Ada,
            Ticker.Link => Link,
            Ticker.Doge => Doge,
            Ticker.Sol => Sol,
            Ticker.Astr => Astr,
            Ticker.BtcJpy => BtcJpy,
            Ticker.EthJpy => EthJpy,
            Ticker.BchJpy => BchJpy,
            Ticker.LtcJpy => LtcJpy,
            Ticker.XrpJpy => XrpJpy,
            Ticker.DotJpy => DotJpy,
            Ticker.AtomJpy => AtomJpy,
            Ticker.AdaJpy => AdaJpy,
            Ticker.LinkJpy => LinkJpy,
            Ticker.DogeJpy => DogeJpy,
            Ticker.SolJpy => SolJpy,
            Ticker.AstrJpy => AstrJpy,
            _ => throw new ArgumentOutOfRangeException(nameof(ticker), ticker, ErrorMessages.TickerHelperInvalidTicker)
        };

    public static Ticker FromString(string ticker) => ticker switch
    {
        Btc => Ticker.Btc,
        Eth => Ticker.Eth,
        Bch => Ticker.Bch,
        Ltc => Ticker.Ltc,
        Xrp => Ticker.Xrp,
        Xem => Ticker.Xem,
        Xlm => Ticker.Xlm,
        Bat => Ticker.Bat,
        Xtz => Ticker.Xtz,
        Qtum => Ticker.Qtum,
        Enj => Ticker.Enj,
        Dot => Ticker.Dot,
        Atom => Ticker.Atom,
        Mkr => Ticker.Mkr,
        Dai => Ticker.Dai,
        Xym => Ticker.Xym,
        Mona => Ticker.Mona,
        Fcr => Ticker.Fcr,
        Ada => Ticker.Ada,
        Link => Ticker.Link,
        Doge => Ticker.Doge,
        Sol => Ticker.Sol,
        Astr => Ticker.Astr,
        BtcJpy => Ticker.BtcJpy,
        EthJpy => Ticker.EthJpy,
        BchJpy => Ticker.BchJpy,
        LtcJpy => Ticker.LtcJpy,
        XrpJpy => Ticker.XrpJpy,
        DotJpy => Ticker.DotJpy,
        AtomJpy => Ticker.AtomJpy,
        AdaJpy => Ticker.AdaJpy,
        LinkJpy => Ticker.LinkJpy,
        DogeJpy => Ticker.DogeJpy,
        SolJpy => Ticker.SolJpy,
        AstrJpy => Ticker.AstrJpy,
        _ => throw new ArgumentOutOfRangeException(nameof(ticker), ticker, ErrorMessages.TickerHelperInvalidTicker)
    };
    
    public class TickerConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(Ticker);

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            var ticker = (Ticker)(value ?? throw new ArgumentNullException(nameof(value)));
            writer.WriteValue(TickerService.ToString(ticker));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var value = reader.Value?.ToString() ?? throw new ArgumentNullException(nameof(reader.Value));
            return FromString(value);
        }
    }
}


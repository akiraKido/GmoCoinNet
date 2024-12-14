using System;
using Newtonsoft.Json;

namespace GmoCoinNet.Schema;

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